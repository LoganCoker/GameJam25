using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {

    #region publics
    public float freq;
    #endregion

    #region privates
    private float startHeight;
    private bool up;
    private bool move;
    #endregion

    void Start() {
        startHeight = transform.position.y;
    }

    void Update() {
        if (!move) {
            if (!up) {
                StartCoroutine(SpikesUp());
                up = true;

            } else {
                StartCoroutine(SpikesDown());
                up = false;
            }
        }
    }
 
    IEnumerator SpikesUp() {
        move = true;
        float timing = 0f;
        while (timing < freq && transform.position.y < startHeight + .2) {
            transform.position += Vector3.up * 0.7f;// timing / (freq);
            timing += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        move = false;  
    }

    IEnumerator SpikesDown() {
        move = true;
        float timing = 0f;
        while (timing < freq && transform.position.y > startHeight) {
            transform.position += Vector3.down * timing / (freq);
            timing += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(freq);
        move = false;
    }   
}
