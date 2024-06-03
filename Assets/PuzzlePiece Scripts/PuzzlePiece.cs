using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    
    private bool dragging;
    private Vector2 offset, originalPosition;private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision Detected");
        // You can add more specific logic here if needed
    }

  [SerializeField] private SpriteRenderer renderer;

    private PuzzleSlot slot;

    public void Init(PuzzleSlot slot)
    {
        renderer.sprite = slot.Renderer.sprite;
        slot = slot;
    }

    private void Awake()
    {
        originalPosition = transform.position;
    }
    private void Update()
    {
        if (!dragging)
            return;

        var mousePosition = GetMousePos();
        transform.position = mousePosition - offset;
    }
    private void OnMouseDown()
    {
        dragging = true;

        offset = GetMousePos() - (Vector2)transform.position;
    }

    private void OnMouseUp()
    {
        transform.position = originalPosition;
        dragging = false;
    }

    Vector2 GetMousePos ()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
