using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodgeable : MonoBehaviour {
   public BossAI Boss;

    void OnTriggerEnter(Collider Obj) {
        if (Obj.CompareTag("Dodge")) {
            Boss.TakeDamage(1);
            Debug.Log("DODGED");
        }
        if (Obj.CompareTag("Parry"))
        {
            hitPlayer.PlayerHealth.SetInvincible(false);
        }
        
        Player hitPlayer = Obj.GetComponent<Player>();
        if (Obj.CompareTag("Player")) {

            hitPlayer.takeDamage(1);
        }
    }
}