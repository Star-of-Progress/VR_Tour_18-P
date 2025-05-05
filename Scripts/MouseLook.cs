using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseLook : MonoBehaviour
{
    // UI slider to adjust mouse sensitivity in real-time
    public Slider slider;

    // Sensitivity multiplier for mouse movement
    public float mouseSensitivity = 100f;

    // Reference to the player's body for rotating the character horizontally
    public Transform playerBody;

    // Tracks the vertical rotation (looking up/down)
    float xRotation = 0f;

    void Start()
    {
        // Load saved sensitivity from PlayerPrefs, or default to 100
        mouseSensitivity = PlayerPrefs.GetFloat("currentSensitivity", 100f);

        // Set the slider value to reflect the loaded sensitivity (divided by 10 for UI scaling)
        slider.value = mouseSensitivity / 10f;

        // Add a listener so moving the slider calls AdjustSpeed()
        slider.onValueChanged.AddListener(AdjustSpeed);

        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Get raw mouse input and apply sensitivity and deltaTime
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Calculate vertical rotation and clamp it to prevent flipping
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Apply vertical rotation to the camera (this GameObject)
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotate the player body horizontally
        playerBody.Rotate(Vector3.up * mouseX);
    }

    // Method called when the slider value changes to update sensitivity
    public void AdjustSpeed(float newSpeed)
    {
        // Multiply slider value by 10 to get the full sensitivity range
        mouseSensitivity = newSpeed * 10f;
    }
}
