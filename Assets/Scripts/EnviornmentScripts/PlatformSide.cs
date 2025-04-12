using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSide : MonoBehaviour
{
    #region publics
    public float dis;
    public float speed;
    public bool MoveX;
    #endregion

    #region privates
    private bool isMoving;
    private Vector3 start;
    private float dest;
    #endregion

    void Start() {
        start = transform.position;
        if (MoveX) {
            dest = start.x + dis;
        } else { 
            dest = start.z + dis;
        }
    }

    void Update() {
        if (!isMoving) {
            if (MoveX) {
                if (transform.position.x <= start.x) {
                    StartCoroutine(MovePosX());
                } else {
                    StartCoroutine(MoveNegX());
                }
            } else {
                if (transform.position.z <= start.z) {
                    StartCoroutine(MovePosZ());
                } else {
                    StartCoroutine(MoveNegZ());
                }
            }
        }
    }

    IEnumerator MovePosX() {
        isMoving = true;
        float timing = 0f;
        while (timing < speed && transform.position.x <= dest) {
            timing += Time.deltaTime;
            transform.position += Vector3.right * 1 / (speed * 20);
            yield return null;
        }
        yield return new WaitForSeconds(speed);
        isMoving = false;
    }

    IEnumerator MoveNegX() {
        isMoving = true;
        float timing = 0f;
        while (timing < speed && transform.position.x > start.x) {
            timing += Time.deltaTime;
            transform.position += Vector3.left * 1 / (speed * 20);
            yield return null;
        }
        yield return new WaitForSeconds(speed);
        isMoving = false;
    }
    
    IEnumerator MovePosZ() {
        isMoving = true;
        float timing = 0f;
        while (timing < speed && transform.position.z <= dest) {
            timing += Time.deltaTime;
            transform.position += Vector3.forward * 1 / (speed * 20);
            yield return null;
        }
        yield return new WaitForSeconds(speed);
        isMoving = false;
    }

    IEnumerator MoveNegZ() {
        isMoving = true;
        float timing = 0f;
        while (timing < speed && transform.position.z > start.z) {
            timing += Time.deltaTime;
            transform.position += Vector3.back * 1 / (speed * 20);
            yield return null;
        }
        yield return new WaitForSeconds(speed);
        isMoving = false;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            other.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            other.transform.SetParent(null);
        }
    }
}
