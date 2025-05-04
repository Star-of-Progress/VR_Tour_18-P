using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Handles player movement including walking, running, jumping, and camera rotation
public class PlayerMovement : MonoBehaviour
{
    // Serialized fields for adjustable movement parameters
    [SerializeField] private float rotate_speed = 75f; // Camera rotation speed
    [SerializeField] private float run_speed = 8;     // Movement speed while running
    [SerializeField] private float walk_speed = 5;     // Movement speed while walking
    [SerializeField] private float jump_force = 5;     // Force applied when jumping
    [SerializeField] private float gravity = -9.81f;   // Gravity force applied to the player
    [SerializeField] private GameObject UI;            // Reference to the UI GameObject

    private CharacterController character_controller; // Reference to the CharacterController component
    private Camera player_camera;                     // Reference to the player's camera

    private Vector3 velocity;    // Stores current movement velocity (x,z) and vertical speed (y)
    private Vector2 direction;    // Stores input direction (x for horizontal, y for vertical)
    private Vector2 rotation;     // Stores camera rotation (x for vertical, y for horizontal)

    // Initialization
    void Start()
    {
        // Get required components
        character_controller = GetComponent<CharacterController>();
        player_camera = GetComponentInChildren<Camera>();
        
        // Initialize game state
        HideCursor(true);    // Lock cursor to game window
        UI.SetActive(false); // Hide UI at start
    }

    // Called every frame
    void Update()
    {
        // Apply current velocity to move the character
        character_controller.Move(velocity * Time.deltaTime);

        // Get input axes
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector2 mouse_delta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        // Handle jumping and gravity
        if (character_controller.isGrounded)
            velocity.y = Input.GetKeyDown(KeyCode.Space) ? jump_force : -0.1f; // Small downward force when grounded
        else
            velocity.y += gravity * Time.deltaTime; // Apply gravity when in air

        // Toggle UI and cursor lock when Escape is pressed
        if (Input.GetKeyDown(KeyCode.Escape)) { 
            UI.SetActive(true);
            HideCursor(false);
        }

        // Calculate camera rotation
        mouse_delta *= rotate_speed * Time.deltaTime;
        rotation.y += mouse_delta.x; // Horizontal rotation (yaw)
        rotation.x = Mathf.Clamp(rotation.x - mouse_delta.y, -90, 90); // Vertical rotation (pitch) clamped to prevent over-rotation
        player_camera.transform.localEulerAngles = rotation; // Apply rotation to camera
    }

    // Called at fixed time intervals (for physics)
    private void FixedUpdate()
    {
        // Apply movement speed (run or walk based on Left Shift)
        direction *= Input.GetKey(KeyCode.LeftShift) ? run_speed : walk_speed;
        
        // Calculate movement direction relative to camera orientation
        Vector3 move = Quaternion.Euler(0, player_camera.transform.eulerAngles.y, 0) * new Vector3(direction.x, 0, direction.y);
        
        // Update velocity (preserving vertical velocity while updating horizontal movement)
        velocity = new Vector3(move.x, velocity.y, move.z); 
    }

    // Controls cursor visibility and lock state
    public void HideCursor(bool state)
    {
        if (state)
        {
            Cursor.lockState = CursorLockMode.Locked; // Lock and hide cursor (for gameplay)
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined; // Show cursor (for UI interaction)
        }
    }
}
