using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float destroyAferTime = 5;

    private Coroutine countdownRoutine;

    private void OnEnable()
    {
        countdownRoutine = StartCoroutine(HandleCountdown());
    }

    public void RestartCountdown(float newDestroyTime)
    {
        destroyAferTime = newDestroyTime;
        StopCoroutine(countdownRoutine);
        countdownRoutine = StartCoroutine(HandleCountdown());
    }

    IEnumerator HandleCountdown()
    {
        yield return new WaitForSeconds(destroyAferTime);
        Destroy(gameObject);
    }


}
