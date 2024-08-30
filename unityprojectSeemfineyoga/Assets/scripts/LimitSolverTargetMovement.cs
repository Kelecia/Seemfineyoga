using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitSolverTargetMovement : MonoBehaviour
{
    public Transform handTransform; // The hand's transform
    public float maxDistance = 0.5f; // Maximum allowed distance from the hand

    void Update()
    {
        // Calculate the distance from the hand
        Vector3 direction = transform.position - handTransform.position;
        if (direction.magnitude > maxDistance)
        {
            // Clamp the position of the solver target to the maxDistance
            transform.position = handTransform.position + direction.normalized * maxDistance;
        }
    }
}
