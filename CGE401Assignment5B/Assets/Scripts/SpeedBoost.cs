/*
 * Matt Schoen
 * SpeedBoost.cs
 * Assignment 6 - Speed Boost power-up
 */

using UnityEngine;
using System.Collections;
using StarterAssets;

public class SpeedBoost : MonoBehaviour, ICollectible
{
    [Header("Power-Up Settings")]
    public float multiplier = 2f;
    public float duration = 10f;
    public Color cubeColor = Color.cyan;

    private bool isCollected = false;

    private void Start()
    {
        // Give cube a bright color for visibility
        GetComponent<Renderer>().material.color = cubeColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Only trigger once, and only for the player
        if (!isCollected && other.CompareTag("Player"))
        {
            Collect(other.GetComponent<MonoBehaviour>());
        }
    }

    public void Collect(MonoBehaviour collector)
    {
        if (isCollected) return;
        isCollected = true;

        // Get the player's controller
        FirstPersonController playerController = collector.GetComponent<FirstPersonController>();

        if (playerController != null)
        {
            // Double their movement and sprint speeds
            float originalMoveSpeed = playerController.MoveSpeed;
            float originalSprintSpeed = playerController.SprintSpeed;

            playerController.MoveSpeed *= multiplier;
            playerController.SprintSpeed *= multiplier;

            Debug.Log("Speed boost activated!");

            // Start coroutine to reset after duration
            StartCoroutine(ResetSpeedAfterDelay(playerController, originalMoveSpeed, originalSprintSpeed));
        }

        // Hide or destroy the cube
        gameObject.SetActive(false);
    }

    private IEnumerator ResetSpeedAfterDelay(FirstPersonController controller, float originalMove, float originalSprint)
    {
        yield return new WaitForSeconds(duration);

        // Restore the speeds
        if (controller != null)
        {
            controller.MoveSpeed = originalMove;
            controller.SprintSpeed = originalSprint;
            Debug.Log("Speed boost ended.");
        }

        // Destroy power-up object after it's used
        Destroy(gameObject);
    }
}
