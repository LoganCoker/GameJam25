using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorMovement : MonoBehaviour {

    #region publics
    public float travDis;
    public float travSpeed;
    public GameObject Boss;
    #endregion

    #region privates
    private float startHeight;
    private float currHeight;
    private bool raising;
    private bool stopZone;
    #endregion

    void Start() {
        startHeight = transform.position.y;
    }

    void Update() {
        currHeight = transform.position.y;
        if (!raising) {
            transform.position -= Vector3.up * travSpeed * Time.deltaTime;
        }
        if (currHeight <= startHeight) {
            raising = true;
        }

    }

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player")) {
            if (currHeight < startHeight + travDis && !stopZone) {
                transform.position += Vector3.up * travSpeed * Time.deltaTime;
            }
            raising = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            raising = false;
        }
        if (other.CompareTag("ElevStopZone")) {
            stopZone = true;
            enabled = false;
            Boss?.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other) {
        
    }
}
