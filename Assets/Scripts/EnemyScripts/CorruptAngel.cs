using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptAngel : MonoBehaviour {
    public Blocker ExitBlocker;
    public Rigidbody rb, PlayerRB;
    public GameObject Player, Enemy;
    public float Speed, RotateSpeed, MaxDistPredict, MinDistPredict, MaxTimePrediction;
    private Vector3 StandardPrediction;

    public AudioSource AudioSource;
    public AudioClip spawnClip;
    public AudioClip grunt;

    public float minGruntInterval = 2f;
    public float maxGruntInterval = 5f;

    
    void Start() {
        PlayerRB = Player.GetComponent<Rigidbody>();
        StartCoroutine(Timer());
        StartCoroutine(PlayGruntAtRandomIntervals());
        AudioSource.spatialBlend = 1f;
        AudioSource.minDistance = 1f;
        AudioSource.maxDistance = 50f;
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
        if (Obj.CompareTag("Dodge")) {
            Enemy.SetActive(false);
            ExitBlocker.IncreaseCounter();
        }
        Player hitPlayer = Obj.GetComponent<Player>();
        if (Obj.CompareTag("Parry"))
        {
            hitPlayer.PlayerHealth.SetInvincible(false);
        }
        if (Obj.CompareTag("Player")) {
            Enemy.SetActive(false);
            hitPlayer.takeDamage(1);
            ExitBlocker.IncreaseCounter();
        }
    }

    public void  PlaySpawnNoise() {
        AudioSource.PlayOneShot(spawnClip, 0.8f);
    }

    public void PlayGrunt() {
        AudioSource.PlayOneShot(grunt, 0.9f);
    }

    IEnumerator Timer() {
        yield return new WaitForSeconds(15);
        Debug.Log("despawning enemy");
        Enemy.SetActive(false);
        ExitBlocker.IncreaseCounter();
    }
    IEnumerator PlayGruntAtRandomIntervals(){
        while (true) {
            float randomInterval = Random.Range(minGruntInterval, maxGruntInterval);
            yield return new WaitForSeconds(randomInterval);
            
            PlayGrunt();
        }
    }
}