using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParry : MonoBehaviour {
    public float ParryCD;
    public float ParryDuration;
    public GameObject ParryObject;
    public KeyCode ParryKey = KeyCode.Mouse0;
    private bool CanParry = true;
    private bool IsParrying = false;
    
    void Update() {  
        if (Input.GetKeyDown(ParryKey)) { Parry(); }
    }

    void Parry() {
        if (!CanParry || IsParrying) { return; }
        StartCoroutine(StartParry());
    }

    IEnumerator StartParry() {
        CanParry = false;
        IsParrying = true;
        ParryObject.SetActive(true);
        yield return new WaitForSeconds(ParryDuration);
        ParryObject.SetActive(false);
        IsParrying = false;
        yield return new WaitForSeconds(ParryCD);
        CanParry = true;
    }
}