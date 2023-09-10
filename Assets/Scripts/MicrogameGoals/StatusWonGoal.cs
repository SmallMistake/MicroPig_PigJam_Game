using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusWonGoal : MicrogameGoalTracker
{
    public void ChangeStatus(bool newStatus)
    {
        goalComplete = newStatus;
        onGoalStatusChanged?.Invoke(goalComplete);
    }
}
