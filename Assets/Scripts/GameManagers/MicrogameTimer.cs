using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MicrogameTimer : MonoBehaviour
{
    public float timeInSeconds;
    public UnityEvent onTimerEnded;

    private Coroutine timerCoroutine;

    private void OnEnable()
    {
        if(timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }
        timerCoroutine = StartCoroutine(HandleTimer());
    }

    private void OnDisable()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }
    }

    IEnumerator HandleTimer()
    {
        yield return new WaitForSeconds(timeInSeconds);
        onTimerEnded?.Invoke();
    }
}
