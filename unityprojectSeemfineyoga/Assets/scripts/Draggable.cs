using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private Rigidbody2D rb;
    private Collider2D col;

    public float detachForce = 10f; // Force applied when detaching
    public ParticleSystem particleEffect; // Reference to the existing particle effect

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found!");
        }

        // Set initial gravity scale to 0 so objects do not fall
        if (rb != null)
        {
            rb.gravityScale = 0f;
        }
    }

    void OnMouseDown()
    {
        isDragging = true;
        offset = transform.position - GetMouseWorldPos();

        // Play the particle effect if the object is tagged as "RightLeg"
        if (gameObject.CompareTag("RightLeg") && particleEffect != null)
        {
            particleEffect.Play(); // Play the particle effect
        }
    }

    void OnMouseUp()
    {
        isDragging = false;

        if (rb != null)
        {
            rb.gravityScale = 1f; // Enable gravity
            rb.AddForce(Vector2.down * detachForce, ForceMode2D.Impulse); // Apply force to simulate falling
        }
    }

    void FixedUpdate()
    {
        if (isDragging && rb != null)
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