using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MicrogameGoalTracker : MonoBehaviour
{
    public UnityEvent<bool> onGoalStatusChanged;
    public bool goalComplete = false;

    public bool getGoalStatus()
    {
        return goalComplete;
    }
}
