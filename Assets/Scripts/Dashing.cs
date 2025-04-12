using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashing : MonoBehaviour {
    [Header("References")]
    public Transform Orientation;
    public Transform PlayerCam;
    private Rigidbody rb;
    private PlayerMovement pm;

    [Header("Dash")]
    public float DashForce;
    public float DashUpwardForce;
    public float DashDuration;

    [Header("Cooldown")]
    public float DashCD;
    private float DashCDTimer;

    [Header("Input")]
    public KeyCode DashKey = KeyCode.E;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement>();
    }

    private void Update() {
        if (Input.GetKeyDown(DashKey)) { Dash(); }
    }

    private void Dash() {
        Vector3 ForceToApply = Orientation.forward * DashForce + Orientation.up * DashUpwardForce;

        rb.AddForce(ForceToApply, ForceMode.Impulse);

        Invoke(nameof(ResetDash), DashDuration);
    }

    private void ResetDash() {
    }
}