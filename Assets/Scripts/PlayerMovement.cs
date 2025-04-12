using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    // Camera Rotation
    public float mouseSensitivity = 2f;
    private float verticalRotation = 0f;
    private Transform cameraTransform;

    // Ground Movement
    private Rigidbody rb;
    public float MoveSpeed = 5f;
    private float moveHorizontal;
    private float moveForward;

    // Jumping movement
    public float jumpForce = 10f;
    // Multiplies gravity when falling
    public float fallMultiplier = 2.5f; 
    // Multiplies gravity when ascending
    public float ascendMultiplier = 2f; 
    private bool isGrounded = true;
    public LayerMask groundLayer;
    private float groundCheckTimer = 0f;
    private float groundCheckDelay = 0.3f;
    private float playerHeight;
    private float raycastDistance;

    // double jump counter
    private int jumpCounter = 1;

    // wall jumps and detection
    public LayerMask wallLayer;
    public float wallCheckDistance = 0.6f;
    private bool isTouchingFrictionWall = false;

    // Sliding
    private bool isSliding;
    public float slideSpeedBoost = 15f;
    private float slideDuration = 0.5f;
    private float timeSinceLanding = 0f;

    [Header("Input")]
    public KeyCode SlideKey = KeyCode.LeftControl;


    // Start is called before the first frame update
    void Start()
    {
        // Get the rigidbody component and freeze its rotation/
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        cameraTransform = Camera.main.transform;

        // set the raycast to be slightly beneath the player's feet
        playerHeight = GetComponent<CapsuleCollider>().height * transform.localScale.y;
        raycastDistance = (playerHeight / 2) + 0.2f;

        // Hides the mouse
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveForward = Input.GetAxisRaw("Vertical");

        RotateCamera();

        if (Input.GetButtonDown("Jump"))
        {   
            if (isGrounded)
            {
                Jump();
            } else if (jumpCounter > 0) {
                Jump();
                jumpCounter--;
            }
        }

        // Check for walls to jump off of, on all sides
        isTouchingFrictionWall = Physics.Raycast(transform.position, transform.right, wallCheckDistance, wallLayer) 
                            || Physics.Raycast(transform.position, -transform.right, wallCheckDistance, wallLayer)
                            || Physics.Raycast(transform.position, transform.forward, wallCheckDistance, wallLayer) 
                            || Physics.Raycast(transform.position, -transform.forward, wallCheckDistance, wallLayer);
        Debug.Log("Is Touching Friction Wall: " + isTouchingFrictionWall);
        // Reset double jump
        if (isTouchingFrictionWall && !isGrounded)
        {
            jumpCounter = 1;
        }

        // Checking when we're on the ground and keeping track of our ground check delay
        if (!isGrounded && groundCheckTimer <= 0f)
        {
            Vector3 rayOrigin = transform.position + Vector3.up * 0.1f;
            isGrounded = Physics.Raycast(rayOrigin, Vector3.down, raycastDistance, groundLayer);
        }
        else
        {
            groundCheckTimer -= Time.deltaTime;
        }

        
        // Checks if you just landed to enable sliding
        if (isGrounded)
        {
            timeSinceLanding += Time.deltaTime;

            if (timeSinceLanding <= 0.25f && Input.GetKeyDown(SlideKey) && !isSliding) {
                Debug.Log("Is pressing ctrl ");
                StartCoroutine(StartSlide());
            }
        } else
        {
            timeSinceLanding = 0;
        }

    }

    void FixedUpdate()
    {
        MovePlayer();
        ApplyJumpPhysics();
    }

    void MovePlayer()
    {
        if (isSliding) return;
        Vector3 movement = (transform.right * moveHorizontal + transform.forward * moveForward).normalized;
        Vector3 targetVelocity = movement * MoveSpeed;

        if (Physics.Raycast(transform.position, movement, out RaycastHit hit, 0.6f))
        {
            // Project movement onto the surface plane to prevent sticking
            movement = Vector3.ProjectOnPlane(movement, hit.normal).normalized;
        }

        // Apply movement to the Rigidbody
        Vector3 velocity = rb.velocity;
        velocity.x = targetVelocity.x;
        velocity.z = targetVelocity.z;
        rb.velocity = velocity;

        // If we aren't moving and are on the ground, stop velocity so we don't slide
        if (isGrounded && moveHorizontal == 0 && moveForward == 0)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }

    void RotateCamera()
    {
        float horizontalRotation = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, horizontalRotation, 0);

        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }

    void Jump()
    {
        isGrounded = false;
        groundCheckTimer = groundCheckDelay;
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        if (jumpCounter <= 0) {
            jumpCounter = 1;
        }
    }

    void ApplyJumpPhysics()
    {
        // Falling
        if (rb.velocity.y < 0) 
        {
            rb.velocity += Vector3.up * Physics.gravity.y * fallMultiplier * Time.fixedDeltaTime;
        } // Rising
        else if (rb.velocity.y > 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * ascendMultiplier  * Time.fixedDeltaTime;
        }
    }

    IEnumerator StartSlide() {
        isSliding = true;
        float startTime = Time.time;

        // use forwards direction and normalize the vector and flatten it
        Vector3 slideDirection = cameraTransform.forward;
        slideDirection.y = 0;
        slideDirection.Normalize();
        
        // start slide
        rb.AddForce(slideDirection * slideSpeedBoost, ForceMode.Impulse);
        yield return new WaitForSeconds(slideDuration);

        isSliding = false;
    }
}
