using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMenu : MonoBehaviour
{
    // Flag to check if the game is currently paused
    public static bool isPaused = false;

    // Reference to the pause menu UI GameObject
    public GameObject UI;

    void Start()
    {
        // When the game starts, make sure it's not paused and the menu is hidden
        isPaused = false;
        UI.SetActive(false);

        // Set the cursor lock state based on pause status
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;

        // Set the game time scale (0 = paused, 1 = normal)
        Time.timeScale = isPaused ? 0f : 1f;
    }

    void Update()
    {
        // Continuously update the cursor lock state based on pause status
        if (isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Listen for the Escape key to toggle pause/resume
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume(); // Resume the game
            }
            else
            {
                Pause(); // Pause the game
            }
        }
    }

    // Resumes the game (hides the menu and unpauses time)
    public void Resume()
    {
        UI.SetActive(false); // Hide the pause menu UI
        isPaused = false;
        Time.timeScale = 1f; // Resume normal game time
    }

    // Pauses the game (shows the menu and freezes time)
    public void Pause()
    {
        UI.SetActive(true); // Show the pause menu UI
        isPaused = true;
        Time.timeScale = 0f; // Freeze game time
    }

    // Method called from the UI "Resume" button — same as Resume()
    public void ResumeButton()
    {
        UI.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
    }
}


