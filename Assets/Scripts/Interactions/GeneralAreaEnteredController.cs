using PixelCrushers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GeneralAreaEnteredController : MonoBehaviour
{
    public bool destroyOnAreaEnter = true;
    public TagMask acceptedTags;

    public UnityEvent<int> onAreaEnter;

    private List<GameObject> objectsInArea = new List<GameObject>(); //Exapnd this later to focus on be able to track if 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(acceptedTags.IsInTagMask(collision.tag))
        {
            objectsInArea.Add(collision.gameObject);
            onAreaEnter?.Invoke(1);
            if (destroyOnAreaEnter)
            {
                Destroy(collision.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!destroyOnAreaEnter && objectsInArea.Contains(collision.gameObject))
        {
            objectsInArea.Remove(collision.gameObject);
            onAreaEnter?.Invoke(-1);
        }
    }
}
