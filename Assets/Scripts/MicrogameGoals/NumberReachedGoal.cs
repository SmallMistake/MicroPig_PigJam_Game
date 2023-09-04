using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberReachedGoal : MicrogameGoalTracker
{
    public int goalNumber;
    public int currentNumber;

    // Update is called once per frame
    void Update()
    {
        if (currentNumber >= goalNumber && !goalComplete)
        {
            goalComplete = true;
            onGoalStatusChanged.Invoke(true);
        }
    }

    public void IncreaseNumber(int amountToIncrease)
    {
        currentNumber += amountToIncrease;
    }
}
