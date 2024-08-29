using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1 : MonoBehaviour
{
    public GameObject musicObject; // Reference to the music GameObject

    public GameObject[] triangles; // Array to hold your triangles
    public Color glowColor; // Color to change to when glowing
    private Color[] originalColors; // To store the original colors of the triangles
    public GameObject yogalady;

    private int[] correctOrder = { 0, 1, 2, 3 }; // The correct order of clicks (indices of triangles)
    private int currentStep = 0; // To track the current step in the sequence

    void Start()
    {
        // Initialize original colors
        originalColors = new Color[triangles.Length];
        for (int i = 0; i < triangles.Length; i++)
        {
            originalColors[i] = triangles[i].GetComponent<SpriteRenderer>().color;
        }
    }

    public void OnTriangleClicked(int index)
    {
        if (index == correctOrder[currentStep])
        {
            // Correct click
            Color newColor = glowColor;
            newColor.a = 1f; // Ensure the color is fully opaque
            triangles[index].GetComponent<SpriteRenderer>().color = glowColor;
            currentStep++;

            // Check if the puzzle is complete
            if (currentStep == correctOrder.Length)
            {
                Debug.Log("Puzzle Completed!");
                PlayYogaladyAnimation();
            }
        }
        else
        {
            // Incorrect click, reset all colors
            ResetColors();
            currentStep = 0;
        }
    }

    private void ResetColors()
    {
        for (int i = 0; i < triangles.Length; i++)
        {
            Color resetColor = originalColors[i];
            resetColor.a = 1f; // Ensure the color is fully opaque
            triangles[i].GetComponent<SpriteRenderer>().color = originalColors[i];
        }
    }

    private void PlayYogaladyAnimation()
    {
        if (yogalady != null)
        {
            Animator animator = yogalady.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("PlayAnimation"); // Trigger the animation
            }
            else
            {
                Debug.LogWarning("No Animator component found on Yogalady.");
            }
        }
        else
        {
            Debug.LogWarning("Yogalady object not assigned.");
        }
        PlayMusic();
    }

    private void PlayMusic()
    {
        if (musicObject != null)
        {
            AudioSource audioSource = musicObject.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.Play(); // Play the music
            }
            else
            {
                Debug.LogWarning("No AudioSource component found on the music object.");
            }
        }
        else
        {
            Debug.LogWarning("Music object not assigned.");
        }
    }
}
