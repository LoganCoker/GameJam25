using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodgeable : MonoBehaviour {
    public GameObject Attack;
   public BossAI Boss;

    void OnTriggerEnter(Collider Obj) {
        if (Obj.CompareTag("Dodge")) {
            Attack.SetActive(false);
            Boss.TakeDamage(1);
            Debug.Log("DODGED");
        }
        Player hitPlayer = Obj.GetComponent<Player>();
        if (Obj.CompareTag("Parry"))
        {
            hitPlayer.PlayerHealth.SetInvincible(false);
        }

        if (Obj.CompareTag("Player")) {

            hitPlayer.takeDamage(1);
        }
    }
}