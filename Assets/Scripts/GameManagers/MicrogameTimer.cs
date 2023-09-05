using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MicrogameTimer : MonoBehaviour
{
    public float timeOutInSeconds;
    public UnityEvent onTimerEnded;

    private float currentTime;

    public delegate void CurrentTimeOutPercent(float currentPercent);
    public static CurrentTimeOutPercent currentTimeOutPercent;

    private void OnEnable()
    {
        currentTime = 0;
    }

    private void FixedUpdate()
    {
        currentTime += Time.deltaTime;
        currentTimeOutPercent?.Invoke(currentTime/timeOutInSeconds);
        if (currentTime >= timeOutInSeconds)
        {
            onTimerEnded?.Invoke();
        }
    }
}
