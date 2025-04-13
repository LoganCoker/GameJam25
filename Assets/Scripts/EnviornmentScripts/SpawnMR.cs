using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMR : MonoBehaviour {
    public BossAI BossAI;
    public MageAI MageAI;

    void Awake() {
        BossAI.enabled = false;
        MageAI.enabled = false;
    }
    void OnTriggerEnter(Collider Obj) {
        if (Obj.CompareTag("Player")) { 
            BossAI.enabled = true;
            MageAI.enabled = true;
        }
    }
}