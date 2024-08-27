using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1 : MonoBehaviour
{
    public GameObject[] triangles; // Array to hold your triangles
    public Color glowColor; // Color to change to when glowing
    private Color[] originalColors; // To store the original colors of the triangles

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
            triangles[index].GetComponent<SpriteRenderer>().color = glowColor;
            currentStep++;

            // Check if the puzzle is complete
            if (currentStep == correctOrder.Length)
            {
                Debug.Log("Puzzle Completed!");
                // Optional: Add code for what happens when the puzzle is solved
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
            triangles[i].GetComponent<SpriteRenderer>().color = originalColors[i];
        }
    }
}
