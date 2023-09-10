using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class MicrogameManager : MonoBehaviour
{
    public List<MicrogameGoalTracker> microgameGoals;

    public delegate void OnMicrogameFinished(bool won);
    public static OnMicrogameFinished onMicrogameFinished;

    private bool gameOver = false;

    private void OnEnable()
    {
        if(microgameGoals.Count == 0)
        {
            microgameGoals = FindObjectsOfType<MicrogameGoalTracker>().ToList();
        }
        foreach(MicrogameGoalTracker microgameGoal in microgameGoals)
        {
            microgameGoal.onGoalStatusChanged.AddListener(GoalStatusChanged);
        }
    }

    public void GoalStatusChanged(bool statusChanged)
    {
        if (!gameOver && IsMicrogameWon())
        {
            print("Microgame Won");
            FinishMicrogame(true);
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
        if(!IsMicrogameWon() && !gameOver)
        {
            print("Time Out");
            FinishMicrogame(false);
        }
    }

    private void FinishMicrogame(bool won)
    {
        gameOver = true;
        onMicrogameFinished?.Invoke(won);
    }
}
