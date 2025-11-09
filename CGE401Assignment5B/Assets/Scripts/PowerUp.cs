/*
 * Matt Schoen
 * PowerUp.cs
 * Assignment 6 - Base class for power-ups (OOP)
 */

using UnityEngine;
using System.Collections;

public abstract class PowerUp : MonoBehaviour, ICollectible
{
    [Header("Power-Up Settings")]
    public float duration = 5f;
    public Color powerUpColor = Color.yellow;

    protected bool isCollected = false;

    protected virtual void Start()
    {
        GetComponent<Renderer>().material.color = powerUpColor;
    }

    // Implement interface method
    public virtual void Collect(MonoBehaviour collector)
    {
        if (isCollected) return;
        isCollected = true;

        // Hide the power-up
        gameObject.SetActive(false);

        // Apply effect and then remove it later
        ApplyEffect(collector);
        collector.StartCoroutine(RemoveAfterDelay(collector));
    }

    protected IEnumerator RemoveAfterDelay(MonoBehaviour collector)
    {
        yield return new WaitForSeconds(duration);
        RemoveEffect(collector);
        Destroy(gameObject);
    }

    public abstract void ApplyEffect(MonoBehaviour collector);
    public abstract void RemoveEffect(MonoBehaviour collector);
}
