using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rightleg : MonoBehaviour
{
    public Transform thighBone;          // The thigh bone to which the lower leg is attached
    public DistanceJoint2D distanceJoint; // Distance joint to keep the bones connected
    public float snapThreshold = 1.0f;  // Distance the player must drag before the limb snaps off

    private bool isDragging = false;
    private Vector3 offset;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main; // Get the main camera for screen to world conversion
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePosition = GetMouseWorldPosition();
            Vector3 newPosition = mousePosition - offset;

            // Calculate the distance between the new position and the thigh bone
            Vector3 direction = newPosition - thighBone.position;
            float distance = direction.magnitude;

            // Constrain the red circle's position within the max distance
            if (distance > distanceJoint.distance)
            {
                newPosition = thighBone.position + direction.normalized * distanceJoint.distance;
            }

            // Move the red circle
            transform.position = newPosition;

            // Check if the player has dragged beyond the snap threshold
            if (distance > snapThreshold)
            {
                SnapLimb();
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

    private void SnapLimb()
    {
        // Disable the distance joint to "snap" the limb off
        distanceJoint.enabled = false;
        // Optionally, handle additional logic for detaching the limb
        gameObject.SetActive(false); // Example: deactivate the limb
    }
}
