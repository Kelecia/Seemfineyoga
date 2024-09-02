using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragRedCircle : MonoBehaviour
{
    public Transform solverTarget;  // Assign the solver target to move
    public Transform bone;          // The bone to which the solver target is attached
    public float maxDistance = 1.0f; // Maximum distance the solver target can move from the bone

    private bool isDragging = false;
    private Vector3 offset;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main; // Get the main camera for screen to world conversion
    }

    void Update()
    {
        // Processing input as early as possible in the Update loop
        if (Input.GetMouseButtonDown(0))
        {
            // Check if the mouse is over this object
            if (IsMouseOverObject())
            {
                OnMouseDown();
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            OnMouseUp();
        }

        // Drag logic
        if (isDragging)
        {
            Vector3 mousePosition = GetMouseWorldPosition();
            Vector3 newPosition = mousePosition - offset;

            // Calculate the distance between the new position and the bone
            Vector3 direction = newPosition - bone.position;
            float distance = direction.magnitude;

            if (distance > maxDistance)
            {
                // Constrain the position within the maximum distance
                newPosition = bone.position + direction.normalized * maxDistance;
            }

            transform.position = newPosition; // Move the red circle
            if (solverTarget != null)
            {
                solverTarget.position = transform.position; // Update the solver target's position
            }
        }
    }

    private void OnMouseDown()
    {
        isDragging = true;
        Vector3 mousePosition = GetMouseWorldPosition();
        offset = mousePosition - transform.position; // Calculate the offset between the click position and the object's position
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = mainCamera.nearClipPlane; // Set the z position to the camera's near clip plane
        return mainCamera.ScreenToWorldPoint(mouseScreenPosition);
    }

    private bool IsMouseOverObject()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

        return hit.collider != null && hit.collider.gameObject == gameObject;
    }
}