using PixelCrushers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour
{
    public string itemName;

    private Rigidbody2D rb;
    private bool dragging = false;
    private List<GameObject> possibleDropLocations = new List<GameObject>();

    private float trueGravityScale;

    public UnityEvent onDragStart;

    [SerializeField]
    private bool unsetParentOnDrag;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        trueGravityScale = rb.gravityScale;
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
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        dragging = true;
        if (unsetParentOnDrag)
        {
            transform.SetParent(null);
        }
        onDragStart?.Invoke();
    }

    public void EndDrag()
    {
        dragging = false;
        if(possibleDropLocations.Count > 0)
        {
            possibleDropLocations[0].GetComponent<DropHolder>().SlotItem(gameObject);
        } else
        {
            rb.gravityScale = trueGravityScale;
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
