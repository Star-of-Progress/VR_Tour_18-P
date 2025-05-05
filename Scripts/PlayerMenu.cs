using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject UI;

    void Start()
    {
        isPaused = false;
        UI.SetActive(false);
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Time.timeScale = isPaused ? 0f : 1f;
    }

    void Update()
    {
        if (isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

    }

    public void Resume()
    {
        UI.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        UI.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;

    }

    public void ResumeButton()
    {
        UI.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
    }



}

