using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float destroyAferTime = 5;
    private void OnEnable()
    {
        StartCoroutine(HandleCountdown());
    }

    IEnumerator HandleCountdown()
    {
        yield return new WaitForSeconds(destroyAferTime);
        Destroy(gameObject);
    }
}
