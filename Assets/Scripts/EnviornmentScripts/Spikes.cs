using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {

    #region publics
    public float freq;
    #endregion

    #region privates
    private float last;
    private float startHeight;
    private float spikeMovement = 0.5f;
    private bool up;
    private bool move;
    #endregion

    void Start() {
        startHeight = transform.position.y;
    }

    void Update() {
        if (!move && last >= spikeMovement) {
            if (!up) {
                StartCoroutine(SpikesUp());
                up = true;
                last = 0;

            } else {
                StartCoroutine(SpikesDown());
                up = false;
                last = 0;

            }
        }
        last += Time.deltaTime;
    }
 
    IEnumerator SpikesUp() {
        move = true;
        while (transform.position.y < startHeight + .2) {
            transform.position += Vector3.up * .7f;
        }
        yield return new WaitForSeconds(spikeMovement);
        move = false;  
    }

    IEnumerator SpikesDown() {
        move = true;
        while (transform.position.y > startHeight) {
            transform.position += Vector3.down * .7f;
        }
        yield return new WaitForSeconds(spikeMovement * freq);
        move = false;
    }   
}
