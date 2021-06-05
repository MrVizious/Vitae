using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool isPaused = false;

    public void Pause() {
        if (Time.timeScale > 0.9f)
        {
            Time.timeScale = 0f;
            isPaused = true;
            UpdateMenu();
        }
    }
    public void Resume() {
        if (Time.timeScale < 0.1f)
        {
            Time.timeScale = 1f;
            isPaused = false;
            UpdateMenu();
        }
    }

    public void SetPause(bool newIsPaused) {
        if (newIsPaused)
        {
            Pause();
        }
        else
        {
            Resume();
        }
    }
    public void TogglePause() {
        SetPause(!isPaused);
    }

    private void UpdateMenu() {
        pauseMenu.SetActive(isPaused);
    }

}
