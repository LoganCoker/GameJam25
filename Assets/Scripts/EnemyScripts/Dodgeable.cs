using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodgeable : MonoBehaviour {
   public BossAI Boss;
    void OnTriggerEnter(Collider Dodge) {
        if (Dodge.CompareTag("Dodge")) {
            Boss.TakeDamage(1);
        }
    }
}