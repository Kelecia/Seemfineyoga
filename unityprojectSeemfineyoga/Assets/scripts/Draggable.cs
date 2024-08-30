using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private Rigidbody2D rb;

    public float detachForce = 10f; // Force applied when detaching

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found!");
        }
    }

    void OnMouseDown()
    {
        isDragging = true;
        offset = transform.position - GetMouseWorldPos();
    }

    void OnMouseUp()
    {
        isDragging = false;

        if (rb != null)
        {
            rb.isKinematic = false; // Ensure Rigidbody is not kinematic
            rb.gravityScale = 1f; // Apply gravity
            rb.AddForce(Vector2.down * detachForce, ForceMode2D.Impulse); // Apply force to simulate falling
        }
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 newPosition = GetMouseWorldPos() + offset;
            rb.MovePosition(newPosition); // Move the Rigidbody2D directly
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector3(mousePoint.x, mousePoint.y, 0);
    }
}
