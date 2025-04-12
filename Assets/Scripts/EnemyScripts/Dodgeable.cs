using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodgeable : MonoBehaviour {
   public GameObject Enemy;
    void OnTriggerEnter(Collider Dodge) {
        if(Dodge.CompareTag("Dodge")) {
            // put damage stuff here
        }
    }
}