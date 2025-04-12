using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorMovement : MonoBehaviour {

    #region publics
    public float travDis;
    public float travSpeed;
    #endregion

    #region privates
    private float startHeight;
    private float currHeight;
    #endregion

    void Start() {
        startHeight = transform.position.y;
    }

    void Update() {
        currHeight = transform.position.y;
    }

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player")) {
            if (currHeight < startHeight + travDis) {
                transform.position += Vector3.up * travSpeed * Time.deltaTime;
            }
        }
    }
}
