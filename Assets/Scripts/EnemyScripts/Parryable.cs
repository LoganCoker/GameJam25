using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parryable : MonoBehaviour {
    public GameObject Enemy;
    void OnTriggerEnter(Collider Parry) {
        if(Parry.CompareTag("Parry")) {
            GetComponentInParent<BossAI>().Health--;
        }
    }
}
