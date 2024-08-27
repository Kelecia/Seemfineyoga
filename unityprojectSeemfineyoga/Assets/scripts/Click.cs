using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    public int index; // The index of this triangle in the puzzle

    void OnMouseDown()
    {
        FindObjectOfType<Puzzle1>().OnTriangleClicked(index);
    }
}
