using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour {
    void OnTriggerEnter(Collider Obj) {
        if (Obj.CompareTag("Player")) {
            Debug.Log("player hit");
        }
    }
}
