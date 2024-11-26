using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteControl : MonoBehaviour
{
    private ColorControlManager colorManager; // Reference to your color manager

    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component

    void Start()
    {
        // Find the ColorControlManager in the scene
        colorManager = FindObjectOfType<ColorControlManager>();

        // Get the SpriteRenderer component attached to the GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Ensure the spriteRenderer is not null
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component is missing on the GameObject!");
        }
    }

    void Update()
    {
        if (colorManager != null && spriteRenderer != null)
        {
            // Update the color of the sprite using values from the color manager
            spriteRenderer.color = new Color(colorManager.rC, colorManager.gC, colorManager.bC);
        }
    }
}
