using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGOD : MonoBehaviour {
    public BossAI BossAI;
    public BackgroundMusic backgroundMusic;
    void Awake() {
        BossAI.enabled = false;
    }
    void OnTriggerEnter(Collider Obj) {
        if (Obj.CompareTag("Player")) { BossAI.enabled = true; backgroundMusic.isBossfightStarted = true; }
    }
}
