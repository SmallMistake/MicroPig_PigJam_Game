using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeOutUIController : MonoBehaviour
{
    [SerializeField]
    Slider progressBar;

    [SerializeField]
    GameObject closeToTimeOutObject;

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

        if (progressBar.value <= 0)
        {
            closeToTimeOutObject.SetActive(true);
        }
    }
}
