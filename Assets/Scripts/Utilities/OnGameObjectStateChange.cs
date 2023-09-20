using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnGameObjectStateChange : MonoBehaviour
{

    public UnityEvent onEnable;
    private void OnEnable()
    {
        onEnable?.Invoke();
    }
}
