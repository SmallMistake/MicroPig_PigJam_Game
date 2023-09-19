using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MicrogameTimer : MonoBehaviour
{
    public float timeOutInSeconds;
    public UnityEvent onTimerEnded;

    private float currentTime;

    private bool paused = false;

    public delegate void CurrentTimeOutPercent(float currentPercent);
    public static CurrentTimeOutPercent currentTimeOutPercent;

    private void OnEnable()
    {

        currentTime = 0;
        MicrogameManager.onMicrogameFinished += HandleFinish;
    }

    private void Update()
    {
        if (!paused)
        {
            currentTime += GameManager.DifficultyDeltaTime;
            currentTimeOutPercent?.Invoke(currentTime / timeOutInSeconds);
            if (currentTime >= timeOutInSeconds)
            {
                onTimerEnded?.Invoke();
            }
        }
    }

    private void HandleFinish(bool won)
    {
        paused = true;
    }
}
