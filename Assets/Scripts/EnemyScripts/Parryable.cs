using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parryable : MonoBehaviour {
    public BossAI Boss;
    void OnTriggerEnter(Collider Parry) {
        if (Parry.CompareTag("Parry")) {
            Boss.TakeDamage(1);
        }
    }
}
