using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageDodgeable : MonoBehaviour {
   public MageAI Boss;

    void OnTriggerEnter(Collider Obj) {
        if (Obj.CompareTag("Dodge")) {
            Boss.TakeDamage(1);
            Debug.Log("DODGED");
        }
        Player hitPlayer = Obj.GetComponent<Player>();
        if (Obj.CompareTag("Player")) {
            hitPlayer.takeDamage(1);
        }
    }
}