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
        // Trigger the animation
        animator.SetTrigger("PlayAnimation");

        FindObjectOfType<Puzzle1>().OnTriangleClicked(index);
    }
}
