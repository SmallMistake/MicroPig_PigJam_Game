using PixelCrushers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour
{
    public string itemName;

    private Rigidbody2D rb;
    private bool dragging = false;
    private List<GameObject> possibleDropLocations = new List<GameObject>();



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DropHolder dropHolder = collision.GetComponent<DropHolder>();
        if(dropHolder != null)
        {
            possibleDropLocations.Add(dropHolder.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (possibleDropLocations.Contains(collision.gameObject))
        {
            possibleDropLocations.Remove(collision.gameObject);
        }
    }

    private void OnMouseDown()
    {
        StartDrag();
    }

    private void OnMouseUp()
    {
        EndDrag();
    }
    public void StartDrag()
    {
        dragging = true;
        transform.SetParent(null);
    }

    public void EndDrag()
    {
        dragging = false;
        if(possibleDropLocations.Count > 0)
        {
            possibleDropLocations[0].GetComponent<DropHolder>().SlotItem(gameObject);
        }
    }
    private void FixedUpdate()
    {
        if (dragging)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            rb.MovePosition(mousePosition);
        }
    }
}
