using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            other.gameObject.transform.position = transform.position;
            other.gameObject.transform.rotation = transform.rotation;
        }
    }
}
