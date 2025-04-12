using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dashing : MonoBehaviour {
    [Header("References")]
    public Transform Orientation;
    public Transform PlayerCam;
    private Rigidbody rb;
    private PlayerMovement pm;

    [Header("Dash")]
    public float DashForce = 100f;
    public float DashUpwardForce = 2f;
    public float DashDuration = 0.2f;

    [Header("Cooldown")]
    public float DashCD = 3f;
    private bool canDash = true;
    private bool isDashing = false;

    [Header("Input")]
    public KeyCode DashKey = KeyCode.LeftShift;

    [Header("UI Elements")]
    public Image DashCooldownImage; 

    private void Start() {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement>();
    }

    private void Update() {
        if (Input.GetKeyDown(DashKey)) { Dash(); }
    }


    void Dash() {
        if (!canDash || isDashing) return;
        StartCoroutine(StartDash());
    }

    IEnumerator StartDash() {
        isDashing = true;
        canDash = false;
        float startTime = Time.time;

        Vector3 forcePerFrame = (Orientation.forward * DashForce + Orientation.up * DashUpwardForce) / DashDuration;
        
        // disablke gravity and movement
        rb.useGravity = false;
        pm.enabled = false;

        // unfill ui when dash starts
        if (DashCooldownImage != null) {
            DashCooldownImage.fillAmount = 0f;
        }

        // start dash
        while (Time.time < startTime + DashDuration)
        {
            rb.AddForce(forcePerFrame * Time.deltaTime, ForceMode.VelocityChange);
            yield return null;
        }

        // finish dash
        pm.enabled = true;
        rb.useGravity = true;
        isDashing = false;

        // start cooldown
        float CooldownTimer = 0f;
        while (CooldownTimer < DashCD){
            CooldownTimer += Time.deltaTime;

            // Update UI based on cooldown progress
            if (DashCooldownImage != null) {
                DashCooldownImage.fillAmount = Mathf.Lerp(0f, 1f, CooldownTimer / DashCD);
            }

            yield return null;
        }
        canDash = true;

    }
}