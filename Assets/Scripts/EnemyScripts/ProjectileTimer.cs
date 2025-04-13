using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTimer : MonoBehaviour {
    public float Timer;
    public GameObject Projectile;

    public AudioSource AudioSource;
    public AudioClip spawnClip;
    public AudioClip fireballClip;

    void Start() {
        Timer = Random.Range(0f, 10f);
        AudioSource = GetComponentInChildren<AudioSource>();
    }
    
    public void  PlaySpawnNoise() {
        AudioSource.PlayOneShot(spawnClip);
    }
    void Update() {
        Timer -= Time.deltaTime;

        if (Timer <= 0) {
            AudioSource.PlayOneShot(fireballClip);
            Projectile.SetActive(true);
        }
    }
}
