using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropHolder : MonoBehaviour
{
    public bool onlyAllowOneItemToBeSlotted;

    private float unslotForce = 250;


    public void SlotItem(GameObject itemToSlot)
    {
        Draggable oldDraggable = GetComponentInChildren<Draggable>();
        if(oldDraggable != null)
        {
            UnslotItem(oldDraggable.gameObject);
        }
        itemToSlot.transform.SetParent(transform);
        itemToSlot.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        itemToSlot.transform.localPosition = Vector3.zero;
    }

    public void UnslotItem(GameObject oldDragable)
    {
        oldDragable.transform.SetParent(null);
        Rigidbody2D oldRigidbody = oldDragable.GetComponent<Rigidbody2D>();
        oldRigidbody.AddForce(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * unslotForce);
    }

}
