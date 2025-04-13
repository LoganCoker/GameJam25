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
        if (Obj.CompareTag("Player")) {
            Debug.Log("player hit");
        }
    }
}