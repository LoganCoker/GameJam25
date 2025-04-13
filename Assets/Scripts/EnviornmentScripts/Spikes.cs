using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {

    #region publics
    public float freq = 1f;
    #endregion

    #region privates
    private float startHeight;
    private bool up;
    private Coroutine currentCoroutine; 
    #endregion

    void Start() {
        startHeight = transform.position.y;
        StartCoroutine(SpikeRoutine());
    }

    IEnumerator SpikeRoutine()
    {
        while (true)
        {
            if (!up)
            {
                if (currentCoroutine != null) {
                    StopCoroutine(currentCoroutine);
                }
                currentCoroutine = StartCoroutine(SpikesUp());
                up = true;
                yield return new WaitForSeconds(1f);
            }
            else
            {
                if (currentCoroutine != null) {
                    StopCoroutine(currentCoroutine);
                }
                currentCoroutine = StartCoroutine(SpikesDown());
                yield return new WaitForSeconds(freq);
                up = false;
            }
        }
    }
 
    IEnumerator SpikesUp() {
        while (transform.position.y < startHeight + .2) {
            transform.position += Vector3.up * 0.7f;
            yield return null;
        }
    }

    IEnumerator SpikesDown() {
        while (transform.position.y > startHeight) {
            transform.position += Vector3.down * 0.8f * Time.deltaTime;
            yield return null;
            
        } 
    }   

    void OnTriggerEnter(Collider Obj) {
        Player hitPlayer = Obj.GetComponent<Player>();
        if (Obj.CompareTag("Player")) {
            hitPlayer.PlayerHealth.SetInvincible(false);
            hitPlayer.takeDamage(1);
        }
    }
}
