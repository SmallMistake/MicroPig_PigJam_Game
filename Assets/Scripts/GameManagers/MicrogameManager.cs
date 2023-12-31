using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class MicrogameManager : MonoBehaviour
{
    private float announcementLength = 1; //TODO use for Microgame Start

    private string microgameName = "Game!!!";

    public List<MicrogameGoalTracker> microgameGoals;

    public delegate void OnMicrogameFinished(bool won);
    public static OnMicrogameFinished onMicrogameFinished;

    public delegate void OnMicrogameAnnounce(string microgameName, float duration);
    public static OnMicrogameAnnounce onMicrogameAnnounce;

    private bool gameOver = false;

    public void SetMicroGameName(string microgameName)
    {
        this.microgameName = microgameName;
    }

    private void Start()
    {

        if(microgameGoals.Count == 0)
        {
            microgameGoals = FindObjectsOfType<MicrogameGoalTracker>().ToList();
        }
        onMicrogameAnnounce?.Invoke(microgameName, announcementLength);

        foreach (MicrogameGoalTracker microgameGoal in microgameGoals)
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

    public bool GetGameOverStatus()
    {
        return gameOver;
    }
}
