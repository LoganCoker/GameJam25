using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fleshball : MonoBehaviour {
    public Blocker ExitBlocker;
    public Rigidbody rb, PlayerRB;
    public GameObject Player, Enemy, Projectile;
    public float Speed, RotateSpeed, MaxDistPredict, MinDistPredict, MaxTimePrediction;
    private Vector3 StandardPrediction;
    
    void Start() {
        PlayerRB = Player.GetComponent<Rigidbody>();
        StartCoroutine(Timer());
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
            Enemy.SetActive(false);
            ExitBlocker.IncreaseCounter();
        }
        Player hitPlayer = Obj.GetComponent<Player>();
        if (Obj.CompareTag("Dodge"))
        {
            hitPlayer.PlayerHealth.SetInvincible(false);
        }
        if (Obj.CompareTag("Player")) {
            hitPlayer.PlayerHealth.SetInvincible(false);
            hitPlayer.takeDamage(1);
            Enemy.SetActive(false);
            ExitBlocker.IncreaseCounter();
        }
    }

    IEnumerator Timer() {
        yield return new WaitForSeconds(15);
        Debug.Log("despawning enemy");
        Enemy.SetActive(false);
        Projectile.SetActive(false);
        ExitBlocker.IncreaseCounter();
    }
}