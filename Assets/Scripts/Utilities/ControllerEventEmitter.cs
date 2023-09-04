using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ControllerEventEmitter : MonoBehaviour
{

    public UnityEvent onPrimaryDown;
    public UnityEvent onPrimaryUp;
    public UnityEvent onSecondaryDown;
    public UnityEvent onSecondaryUp;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Primary"))
        {
            onPrimaryDown?.Invoke();
        }
        if (Input.GetButtonUp("Primary"))
        {
            onPrimaryUp?.Invoke();
        }
        if (Input.GetButtonDown("Secondary"))
        {
            onSecondaryDown?.Invoke();
        }
        if (Input.GetButtonDown("Secondary"))
        {
            onSecondaryUp?.Invoke();
        }
    }
}
