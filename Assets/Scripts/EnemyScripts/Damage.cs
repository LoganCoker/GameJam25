using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour {

    void OnTriggerEnter(Collider Obj) {
        Player hitPlayer = Obj.GetComponent<Player>();
        if (Obj.CompareTag("Player")) {
            hitPlayer.PlayerHealth.SetInvincible(false);
            hitPlayer.takeDamage(1);
        }
    }
}
