using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMR : MonoBehaviour {
    public BossAI BossAI;
    public MageAI MageAI;
    public GameObject EntranceBarrier, ExitBarrier;

    void Awake() {
        BossAI.enabled = false;
        MageAI.enabled = false;
        EntranceBarrier.SetActive(false);
        ExitBarrier.SetActive(false);
    }
    void OnTriggerEnter(Collider Obj) {
        if (Obj.CompareTag("Player")) { 
            BossAI.enabled = true;
            MageAI.enabled = true;
            EntranceBarrier.SetActive(true);
            ExitBarrier.SetActive(true);
        }
    }
}