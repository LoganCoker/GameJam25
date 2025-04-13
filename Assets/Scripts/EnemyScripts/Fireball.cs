using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour {
    public MageAI Boss;
    public Rigidbody rb;
    public GameObject Projectile;
    public float Speed;
    
    void Start() {
        rb.velocity = transform.forward * Speed;
    }

    void OnTriggerEnter(Collider Obj) {
        if (Obj.CompareTag("Parry")) {
            Projectile.SetActive(false);
            Boss.TakeDamage(1);
            Debug.Log("PARRIED");
        }
        Player hitPlayer = Obj.GetComponent<Player>();
        if (Obj.CompareTag("Player")) {
            Projectile.SetActive(false);
            hitPlayer.takeDamage(1);
        }
    }
}