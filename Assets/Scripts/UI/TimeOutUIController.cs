using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeOutUIController : MonoBehaviour
{
    public Slider progressBar;

    private void OnEnable()
    {
        MicrogameTimer.currentTimeOutPercent += UpdateVisuals;
    }

    private void OnDisable()
    {
        MicrogameTimer.currentTimeOutPercent -= UpdateVisuals;
    }

    private void UpdateVisuals(float currentPercent)
    {
        progressBar.value = 1 - currentPercent;
    }
}
