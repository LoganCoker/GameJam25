using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parryable : MonoBehaviour {
    public BossAI Boss;
    void OnTriggerEnter(Collider Obj) {
        if (Obj.CompareTag("Parry")) {
            Boss.TakeDamage(1);
            Debug.Log("PARRIED");
        }
        if (Obj.CompareTag("Dodge"))
        {
            hitPlayer.PlayerHealth.SetInvincible(false);
        }
        Player hitPlayer = Obj.GetComponent<Player>();
        if (Obj.CompareTag("Player")) {
            hitPlayer.takeDamage(1);
        }
    }
}
