using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour {
    [Header("Corrupt Angels")]
    public CorruptAngel EyeOne, EyeTwo, EyeThree;
    [Header("Centipedes")]
    public ProjectileTimer CentOne, CentTwo, CentThree;

    void Awake() {
        if (EyeOne != null) { EyeOne.enabled = false; }
        if (EyeTwo != null) { EyeTwo.enabled = false; }
        if (EyeThree != null) { EyeThree.enabled = false; }

        if (CentOne != null) { CentOne.enabled = false; }
        if (CentTwo != null) { CentTwo.enabled = false; }
        if (CentThree != null) { CentThree.enabled = false; }
    }

    void OnTriggerEnter(Collider Obj) {
        if (Obj.CompareTag("Player")) {
        if (EyeOne != null) { EyeOne.enabled = true; }
        if (EyeTwo != null) { EyeTwo.enabled = true; }
        if (EyeThree != null) { EyeThree.enabled = true; }

        if (CentOne != null) { CentOne.enabled = true; }
        if (CentTwo != null) { CentTwo.enabled = true; }
        if (CentThree != null) { CentThree.enabled = true; }
    }
    }
}
