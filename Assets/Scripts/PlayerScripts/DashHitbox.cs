using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashHitbox : MonoBehaviour {
    public float DashCD;
    public float DashDuration;
    public GameObject DashObject;
    public KeyCode DashKey = KeyCode.LeftShift;
    private bool CanDash = true;
    private bool IsDashing = false;

    void Update() {  
        if (Input.GetKeyDown(DashKey)) { Dash(); }
    }

    void Dash() {
        if (!CanDash || IsDashing) { return; }
        StartCoroutine(StartDash());
    }

    IEnumerator StartDash() {
        CanDash = false;
        IsDashing = true;
        DashObject.SetActive(true);
        yield return new WaitForSeconds(DashDuration);
        DashObject.SetActive(false);
        IsDashing = false;
        yield return new WaitForSeconds(DashCD);
        CanDash = true;
    }
}
