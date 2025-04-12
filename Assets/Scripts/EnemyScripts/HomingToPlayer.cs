using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingToPlayer : MonoBehaviour {
    public Rigidbody rb, PlayerRB;
    public GameObject Player, Enemy, Projectile;
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
}