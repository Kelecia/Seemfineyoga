using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    public int index; // The index of this triangle in the puzzle

    private Animator animator;

    void Start()
    {
        // Get the Animator component attached to this GameObject
        animator = GetComponent<Animator>();
    }

    void OnMouseDown()
    {
        // Check if the puzzle is completed
        Puzzle1 puzzle = FindObjectOfType<Puzzle1>();
        if (puzzle != null && puzzle.IsPuzzleCompleted())
        {
            return; // Puzzle is completed, do not play the animation
        }

        // Trigger the animation if the puzzle is not completed
        animator.SetTrigger("PlayAnimation");

        // Notify the puzzle manager of the click
        puzzle.OnTriangleClicked(index);
    }
}

