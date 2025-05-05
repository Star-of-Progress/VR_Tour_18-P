using UnityEngine;

// Ensures the GameObject has a CharacterController component
[RequireComponent(typeof(CharacterController))]
public class SimpleMovementNoGroundCheck : MonoBehaviour
{
    // Movement speed of the player
    [SerializeField] private float moveSpeed = 5f;

    // Force applied when jumping
    [SerializeField] private float jumpForce = 5f;

    // Gravity applied to the player (should be negative)
    [SerializeField] private float gravity = -9.81f;

    // Reference to the CharacterController component
    private CharacterController controller;

    // Velocity vector, mainly for vertical motion (jumping/falling)
    private Vector3 velocity;

    // Tracks whether the player is currently jumping
    private bool jumping = false;

    void Start()
    {
        // Get and store the CharacterController component
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Get input from keyboard (WASD or arrow keys)
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Calculate movement direction relative to the player's orientation
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Apply horizontal movement
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Jump input — only if not already jumping
        if (Input.GetKeyDown(KeyCode.Space) && !jumping)
        {
            velocity.y = jumpForce;
            jumping = true;
        }

        // Apply gravity over time
        velocity.y += gravity * Time.deltaTime;

        // Apply vertical movement (falling/jumping)
        controller.Move(velocity * Time.deltaTime);

        // Check if player has landed (touching the ground and falling)
        if ((controller.collisionFlags & CollisionFlags.Below) != 0 && velocity.y < 0)
        {
            velocity.y = 0f; // Reset vertical velocity
            jumping = false; // Allow jumping again
        }
    }
}
