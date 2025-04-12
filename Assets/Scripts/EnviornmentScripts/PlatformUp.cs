using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformUp : MonoBehaviour {

    #region publics
    public float dis;
    public float speed;
    #endregion

    #region privates
    private bool isMoving;
    private Vector3 start;
    private float dest;
    #endregion

    void Start() {
        start = transform.position;
        dest = start.y + dis;
    }

    void Update() {
        if (!isMoving) {
            if (transform.position.y <= start.y) {
                StartCoroutine(MoveUp());
            } else {
                StartCoroutine(MoveDown()); 
            }
        }
    }

    IEnumerator MoveUp() { 
        isMoving = true;
        float timing = 0f;
        while (timing < speed && transform.position.y <= dest) {
            timing += Time.deltaTime;
            transform.position += Vector3.up * 1/(speed * 20);
            yield return null; 
        }
        yield return new WaitForSeconds(speed);
        isMoving = false;
    }

    IEnumerator MoveDown() {
        isMoving = true;
        float timing = 0f;
        while (timing < speed && transform.position.y > start.y) {
            timing += Time.deltaTime;
            transform.position += Vector3.down * 1 / (speed * 20);
            yield return null;
        }
        yield return new WaitForSeconds(speed);
        isMoving = false;
    }
}
 