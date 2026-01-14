using UnityEngine;
/// <summary>
/// Handles basic first-person or third-person player movement in Unity.
/// Attach this script to a GameObject with a CharacterController.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f;       // Walking speed
    public float runSpeed = 8f;        // Running speed
    public float jumpHeight = 2f;      // Jump height in meters
    public float gravity = -9.81f;     // Gravity force

    [Header("Ground Check Settings")]
    public Transform groundCheck;      // Empty GameObject at player's feet
    public float groundDistance = 0.4f;
    public LayerMask groundMask;       // Layer(s) considered as ground

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // Validate required components
        if (groundCheck == null)
        {
            Debug.LogError("GroundCheck Transform is not assigned.");
        }
    }

    void Update()
    {
        // Ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Reset downward velocity when grounded
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Get input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Determine movement direction relative to player orientation
        Vector3 move = transform.right * x + transform.forward * z;

        // Run toggle (Shift key)
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        // Apply movement
        controller.Move(move * currentSpeed * Time.deltaTime);

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}