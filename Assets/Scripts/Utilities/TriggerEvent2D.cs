using PixelCrushers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent2D : MonoBehaviour
{
    [SerializeField]
    TagMask acceptedTags;

    public UnityEvent<Collider2D> onTriggerEnter;
    public UnityEvent<Collider2D> onTriggerExit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( acceptedTags.IsInTagMask(collision.tag ))
        {
            onTriggerEnter?.Invoke(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (acceptedTags.IsInTagMask(collision.tag))
        {
            onTriggerExit?.Invoke(collision);
        }
    }
}
