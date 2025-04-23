using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float rotate_speed = 75f;
    [SerializeField] private float run_speed = 8;
    [SerializeField] private float walk_speed = 5;
    [SerializeField] private float jump_force = 5;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private GameObject UI;

    private CharacterController character_controller;
    private Camera player_camera;

    private Vector3 velocity;
    private Vector2 direction;
    private Vector2 rotation;

    // Start is called before the first frame update
    void Start()
    {
        character_controller = GetComponent<CharacterController>();
        player_camera = GetComponentInChildren<Camera>();
        HideCursor(true);
        UI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        character_controller.Move(velocity * Time.deltaTime);

        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector2 mouse_delta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        if (character_controller.isGrounded) velocity.y = Input.GetKeyDown(KeyCode.Space) ? jump_force : -0.1f;
        else velocity.y += gravity * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Escape)) { 
            UI.SetActive(true);
            HideCursor(false);
        }

        mouse_delta *= rotate_speed * Time.deltaTime;
        rotation.y += mouse_delta.x;
        rotation.x = Mathf.Clamp(rotation.x - mouse_delta.y, -90, 90);
        player_camera.transform.localEulerAngles = rotation;
    }

    private void FixedUpdate()
    {
        direction *= Input.GetKey(KeyCode.LeftShift) ? run_speed : walk_speed;
        Vector3 move = Quaternion.Euler(0, player_camera.transform.eulerAngles.y, 0) * new Vector3(direction.x, 0, direction.y);
        velocity = new Vector3(move.x, velocity.y, move.z); 
    }
    public void HideCursor (bool state)
    {
        if (state)
        {
            Cursor.lockState = CursorLockMode.Locked;
        } else
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
}
