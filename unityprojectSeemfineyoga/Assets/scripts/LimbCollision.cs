using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbCollision : MonoBehaviour
{
    // Tag of the symbol this limb needs to touch
    public string targetTag;

    // Static variable to track the number of correct matches
    private static int correctMatches = 0;

    // The total number of correct symbols to match
    public static int totalCorrectSymbols = 3;

    // Animator to play the completion animation
    public Animator externalAnimator;

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the limb's red circle is touching the correct symbol
        if (other.CompareTag(targetTag))
        {
            Debug.Log("Limb touched the correct symbol!");

            // Make the limb a child of the symbol
            transform.SetParent(other.transform);

            // Snap the limb to the symbol's position
            SnapToSymbol(other.transform);

            // Disable dragging scripts
            DisableDragging();

            // Increment the correct match counter
            correctMatches++;

            // Check if the puzzle is complete
            if (correctMatches >= totalCorrectSymbols)
            {
                PlayCompletionAnimation();
            }
        }
    }

    // Snaps the limb to the symbol's position
    void SnapToSymbol(Transform symbolTransform)
    {
        transform.position = symbolTransform.position;
        transform.rotation = symbolTransform.rotation;
    }

    // Disables both drag scripts
    void DisableDragging()
    {
        // Access and disable the Draggable script
        Draggable draggableScript = GetComponent<Draggable>();
        if (draggableScript != null)
        {
            draggableScript.enabled = false;
        }

        // Access and disable the DragRedCircle script
        DragRedCircle dragRedCircleScript = GetComponent<DragRedCircle>();
        if (dragRedCircleScript != null)
        {
            dragRedCircleScript.enabled = false;
        }
    }

    // Plays the completion animation when the puzzle is complete
    void PlayCompletionAnimation()
    {
        if (externalAnimator != null)
        {
            externalAnimator.SetTrigger("PuzzleComplete");
        }
        else
        {
            Debug.LogError("External Animator is not assigned.");
        }
    }
}
