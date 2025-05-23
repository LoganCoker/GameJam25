using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parryable : MonoBehaviour {
    public GameObject Attack;
    public BossAI Boss;
    void OnTriggerEnter(Collider Obj) {
        if (Obj.CompareTag("Parry")) {
            Attack.SetActive(false);
            Boss.TakeDamage(1);
            Debug.Log("PARRIED");
        }
        Player hitPlayer = Obj.GetComponent<Player>();
        if (Obj.CompareTag("Dodge"))
        {
            hitPlayer.PlayerHealth.SetInvincible(false);
        }
        if (Obj.CompareTag("Player")) {
            hitPlayer.takeDamage(1);
        }
    }
}
