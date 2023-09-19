using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MicrogameGoalTracker : MonoBehaviour
{
    public UnityEvent<bool> onGoalStatusChanged;
    public bool goalComplete = false;

    public void CompleteGoal(bool winStatus)
    {
        goalComplete = true;
        onGoalStatusChanged.Invoke(winStatus);
    }

    public bool getGoalStatus()
    {
        return goalComplete;
    }
}
