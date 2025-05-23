using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GODBall : MonoBehaviour {
    public HealthSystem BossHealth;
    public Rigidbody rb, PlayerRB;
    public GameObject Player, Projectile;
    public float Speed, RotateSpeed, MaxDistPredict, MinDistPredict, MaxTimePrediction;
    private Vector3 StandardPrediction;
    
    void Start() {
        PlayerRB = Player.GetComponent<Rigidbody>();
    }

    void FixedUpdate() { 
        rb.velocity = transform.forward * Speed;
        
        var LeadTimePercentage = Mathf.InverseLerp(MinDistPredict, MaxDistPredict, 
        Vector3.Distance(transform.position, Player.transform.position));

        PredictMovement(LeadTimePercentage);
        RotateEnemy();
    }

    private void PredictMovement(float LeadTimePercentage) {
        var PredictionTime = Mathf.Lerp(0, MaxTimePrediction, LeadTimePercentage);
        StandardPrediction = PlayerRB.position + PlayerRB.velocity * PredictionTime;
    }

    private void RotateEnemy() {
        Vector3 Direction = (StandardPrediction - transform.position).normalized;
        var Rotation = Quaternion.LookRotation(Direction);
        rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, Rotation, RotateSpeed * Time.fixedDeltaTime));
    }

    void OnTriggerEnter(Collider Obj) {
        if (Obj.CompareTag("Parry")) {
            BossHealth.LoseHealth(1);
            Projectile.SetActive(false);
        }
        Player hitPlayer = Obj.GetComponent<Player>();
        if (Obj.CompareTag("Dodge"))
        {
            hitPlayer.PlayerHealth.SetInvincible(false);
        }
        if (Obj.CompareTag("Player")) {
            Projectile.SetActive(false);
            hitPlayer.takeDamage(1);
        }
    }
}