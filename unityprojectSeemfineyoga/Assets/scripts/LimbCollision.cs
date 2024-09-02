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

    // Color to change the symbol to when matched
    public Color matchedColor = Color.green;

    // Original color of the symbol
    private Color originalColor;

    void Start()
    {
        // If there is a SpriteRenderer on the same object, get the original color
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

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

            // Change the color of the symbol to the matched color
            ChangeSymbolColor(other.gameObject);

            // Play the animation on the symbol
            PlaySymbolAnimation(other.gameObject);

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

    // Changes the color of the symbol when matched
    void ChangeSymbolColor(GameObject symbol)
    {
        SpriteRenderer symbolRenderer = symbol.GetComponent<SpriteRenderer>();
        if (symbolRenderer != null)
        {
            symbolRenderer.color = matchedColor;
        }
    }

    // Plays a short animation on the symbol
    void PlaySymbolAnimation(GameObject symbol)
    {
        Animator symbolAnimator = symbol.GetComponent<Animator>();
        if (symbolAnimator != null)
        {
            symbolAnimator.SetTrigger("Correct");
        }
        else
        {
            Debug.LogWarning("Animator component not found on the symbol.");
        }
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
