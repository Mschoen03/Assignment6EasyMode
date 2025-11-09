/*
 * Matt Schoen
 * PauseController.cs
 * Assignment 6 - Pause menu. Toggles pause UI and controls Time.timeScale.
 */

using UnityEngine;

public class PauseController : MonoBehaviour
{
    public GameObject pausePanel; // contains resume/main menu buttons

    private bool isPaused = false;

    void Start()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);
    }

    void Update()
    {
        // Toggle pause with P or Escape
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        SetPaused(isPaused);
    }

    public void SetPaused(bool paused)
    {
        isPaused = paused;
        if (pausePanel != null)
            pausePanel.SetActive(paused);

        Time.timeScale = paused ? 0f : 1f;
        Cursor.visible = paused;
        Cursor.lockState = paused ? CursorLockMode.None : CursorLockMode.Locked;
    }

    // Resume button can call this method
    public void Resume()
    {
        SetPaused(false);
    }

    // Main menu button calls GameManager.Instance.ReturnToMainMenu()
    public void ReturnToMainMenu()
    {
        SetPaused(false);

        // Ensure GameManager exists
        if (Manager.Instance != null)
        {
            // Update the main menu scene name just in case
            Manager.Instance.mainMenuSceneName = "Menu";
            Manager.Instance.ReturnToMainMenu();
        }
        else
        {
            Debug.LogWarning("GameManager.Instance is null — returning to menu using fallback SceneManager.");
            // Fallback if GameManager isn't in the scene
            UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
        }
    }
}
