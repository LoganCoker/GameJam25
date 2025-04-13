using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTimer : MonoBehaviour {
    public float Timer;
    public GameObject Projectile;

    public AudioSource AudioSource;
    public AudioClip spawnClip;
    public AudioClip fireballClip;
    public Animator CentipedeAnimator;

    void Start() {
        Timer = Random.Range(0f, 10f);
        AudioSource = GetComponentInChildren<AudioSource>();
        AudioSource.spatialBlend = 1f;
        AudioSource.minDistance = 1f;
        AudioSource.maxDistance = 50f;
    }
    
    public void  PlaySpawnNoise() {
        AudioSource.PlayOneShot(spawnClip, 0.7f);
    }
    void Update() {
        Timer -= Time.deltaTime;

        if (Timer <= 0) {
            StartCoroutine(Attack());
        }


    }

    IEnumerator Attack() {
        CentipedeAnimator.SetTrigger("IsAttacking");
        yield return new WaitForSeconds(2);
        AudioSource.PlayOneShot(fireballClip);
        Projectile.SetActive(true);
    }
}   
