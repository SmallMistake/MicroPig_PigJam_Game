using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMicrogameWaitGoal : MicrogameGoalTracker
{

    private void Start()
    {
        this.goalComplete = true;
        Invoke("EndGame", 3);
    }

    void EndGame()
    {
        this.onGoalStatusChanged?.Invoke(true);
    }
}
