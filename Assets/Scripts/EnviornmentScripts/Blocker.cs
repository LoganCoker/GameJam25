using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocker : MonoBehaviour {
    public GameObject ExitBlocker;
    public int DeathCounter, RequiredDeath;
    void Awake() {
        ExitBlocker.SetActive(true);
        DeathCounter = 0;
    }

    void Update() {
        if (DeathCounter >= RequiredDeath) { ExitBlocker.SetActive(false); }
    }

    public void IncreaseCounter() {
        DeathCounter++;
    }
}
