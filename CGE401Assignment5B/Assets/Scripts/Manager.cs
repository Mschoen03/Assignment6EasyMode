/*
 * Matt Schoen
 * GameManager.cs
 * Assignment 6 - Singleton Game Manager: loads and unloads scenes and returns to main menu
 * Async scene loading and unloading game state control (pause/unpause).
 */

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public static Manager Instance { get; private set; }

    [Header("Main Menu")]
    public string mainMenuSceneName = "MainMenu"; // set in inspector or leave default

    void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Public entry point for UI buttons and other scripts:
    public void LoadLevel(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    public void UnloadLevel(string sceneName)
    {
        StartCoroutine(UnloadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        op.allowSceneActivation = true;

        // wait until the async load is finished
        while (!op.isDone)
        {
            yield return null;
        }
    }

    private IEnumerator UnloadSceneAsync(string sceneName)
    {
        if (!SceneManager.GetSceneByName(sceneName).isLoaded)
            yield break;

        AsyncOperation op = SceneManager.UnloadSceneAsync(sceneName);
        while (!op.isDone)
        {
            yield return null;
        }
    }

    // Called by Pause menu if user returns to main menu
    public void ReturnToMainMenu()
    {
        // make sure game is unpaused
        Time.timeScale = 1f;
        LoadLevel(mainMenuSceneName);
    }
}
