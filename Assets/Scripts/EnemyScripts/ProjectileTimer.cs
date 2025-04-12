using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTimer : MonoBehaviour {
    public float Timer;
    public GameObject Projectile;
    void Start() {
        
    }

    void Update() {
        Timer -= Time.deltaTime;

        if (Timer <= 0) {
            Projectile.SetActive(true);
            Timer = 15;
        }
    }
}
