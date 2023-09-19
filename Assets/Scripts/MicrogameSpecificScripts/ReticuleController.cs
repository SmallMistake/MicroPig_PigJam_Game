using PixelCrushers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ReticuleController : MonoBehaviour
{
    [SerializeField]
    Animator reticuleAnimator;

    [SerializeField]
    TagMask acceptedTags;

    private List<GameObject> gameObjectsInReticule = new List<GameObject>();

    [SerializeField]
    UnityEvent<List<GameObject>> onHit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (acceptedTags.IsInTagMask(collision.gameObject.tag))
        {
            if (!gameObjectsInReticule.Contains(collision.gameObject))
            {
                gameObjectsInReticule.Add(collision.gameObject);
                reticuleAnimator.SetBool("IsTargetInRange", true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (acceptedTags.IsInTagMask(collision.gameObject.tag))
        {
            if (gameObjectsInReticule.Contains(collision.gameObject))
            {
                gameObjectsInReticule.Remove(collision.gameObject);
            }
        }

        if(gameObjectsInReticule.Count <= 0)
        {
            reticuleAnimator.SetBool("IsTargetInRange", false);
        }
    }

    public void Fire()
    {
        if (gameObjectsInReticule.Count > 0)
        {
            onHit?.Invoke(gameObjectsInReticule);
        }
    }

}
