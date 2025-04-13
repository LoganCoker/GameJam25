using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTimer : MonoBehaviour {
    public float Timer;
    public GameObject Projectile;
    void Start() {
        Timer = Random.Range(0f, 10f);
    }

    void Update() {
        Timer -= Time.deltaTime;

        if (Timer <= 0) {
            Projectile.SetActive(true);
        }
    }
}
