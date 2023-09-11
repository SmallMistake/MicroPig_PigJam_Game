using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropHolderFilledGoal : MicrogameGoalTracker
{
    public List<string> acceptedItemNames = new List<string>();

    public void CheckSlottedItem(string slottedItemName)
    {
        if(acceptedItemNames.Count == 0 || acceptedItemNames.Contains(slottedItemName))
        {
            goalComplete = true;
            onGoalStatusChanged.Invoke(true);
        }
        else
        {
            goalComplete = false;
        }
    }
}
