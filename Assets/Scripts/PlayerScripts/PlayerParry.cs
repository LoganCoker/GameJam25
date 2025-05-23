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
    private Player player;
    private Animator animator;

    void Start() {
        player = GetComponent<Player>();
        animator = GetComponentInChildren<Animator>();
    }
    
    void Update() {  
        if (Input.GetKeyDown(ParryKey)) { Parry(); }
    }

    void Parry() {
        if (!CanParry || IsParrying) { return; }
        animator.SetTrigger("Parry");
        StartCoroutine(StartParry());
    }

    IEnumerator StartParry() {
        CanParry = false;
        IsParrying = true;
        player.PlayerHealth.SetInvincible(true);
        ParryObject.SetActive(true);
        yield return new WaitForSeconds(ParryDuration);
        ParryObject.SetActive(false);
        IsParrying = false;
        yield return new WaitForSeconds(ParryCD);
        player.PlayerHealth.SetInvincible(false);
        CanParry = true;
    }
}