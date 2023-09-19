using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DropHolder : MonoBehaviour
{
    private float unslotForce = 250;
    public bool addUnslotForce = false;

    public UnityEvent<string> onHolderFilled;

    public void SlotItem(GameObject itemToSlot)
    {
        Draggable oldDraggable = GetComponentInChildren<Draggable>();
        if(oldDraggable != null)
        {
            UnslotItem(oldDraggable.gameObject);
        }
        itemToSlot.transform.SetParent(transform);
        itemToSlot.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        itemToSlot.GetComponent<Rigidbody2D>().gravityScale = 0;
        itemToSlot.transform.localPosition = Vector3.zero;
        itemToSlot.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        onHolderFilled?.Invoke(itemToSlot.GetComponent<Draggable>().itemName);
    }

    public void UnslotItem(GameObject oldDragable)
    {
        oldDragable.transform.SetParent(null);
        oldDragable.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        if (addUnslotForce)
        {
            Rigidbody2D oldRigidbody = oldDragable.GetComponent<Rigidbody2D>();
            oldRigidbody.AddForce(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * unslotForce);
        }
    }

}
