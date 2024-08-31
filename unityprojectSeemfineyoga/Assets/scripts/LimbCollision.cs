using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbCollision : MonoBehaviour
{
    // Tag of the symbol this limb needs to touch
    public string targetTag;

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

            // Optionally, disable the drag script or other interactions
            DisableDragging();
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
}
