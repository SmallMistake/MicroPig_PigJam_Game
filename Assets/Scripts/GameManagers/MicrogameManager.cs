using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrogameManager : MonoBehaviour
{
    public float microGameLengthInSeconds;
    public List<MicrogameGoalTracker> microgameGoals;

    private void OnEnable()
    {
        foreach(MicrogameGoalTracker microgameGoal in microgameGoals)
        {
            microgameGoal.onGoalStatusChanged.AddListener(GoalStatusChanged);
        }
        /*
        if(microgameCountDown != null)
        {
            StopCoroutine(microgameCountDown);
            microgameCountDown = null;
        }
        */
    }

    public void GoalStatusChanged(bool statusChanged)
    {
        if (IsMicrogameWon())
        {
            print("Microgame Won");
        }
    }

    private bool IsMicrogameWon()
    {
        bool microgameWon = true;
        foreach (MicrogameGoalTracker microgameGoal in microgameGoals)
        {
            if (microgameGoal.getGoalStatus() == false)
            {
                microgameWon = false;
            }
        }

        return microgameWon;
    }

    public void TimeOut()
    {
        print("Time Out");
    }
}
