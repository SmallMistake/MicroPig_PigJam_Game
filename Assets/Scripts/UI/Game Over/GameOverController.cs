using Febucci.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class Rankings {
    public string rankName;
    public int rankRequirements;
}



public class GameOverController : MonoBehaviour
{
    [SerializeField]
    List<Rankings> rankings = new List<Rankings>();

    [SerializeField]
    TypewriterByCharacter gameOverTypeWriter;

    [SerializeField]
    TextMeshProUGUI levelTextMesh;

    [SerializeField]
    Animator gameOverAnimator;

    private void OnEnable()
    {
        HandleGameOver();
    }

    public void HandleGameOver()
    {
        int levelsCompleted = GetLevelsCompleted();
        levelTextMesh.text = levelsCompleted.ToString();
        string ranking = GetRanking(levelsCompleted);
        gameOverAnimator.SetTrigger("ShowGameOverPanel");
    }

    public void WriteGameOverText()
    {
        gameOverTypeWriter.StartShowingText(restart: true);
    }

    public void SetAnimatorTrigger(string triggerName)
    {
        gameOverAnimator.SetTrigger(triggerName);
    }

    private int GetLevelsCompleted()
    {
        if (GameManager.Exists)
        {
            return GameManager.CompletedGames;
        }
        else // Used for debuging when a game manager might not exist.
        {
            return 0;
        }
    }

    private string GetRanking(int levelsCompleted)
    {
        foreach (Rankings ranking in rankings)
        {
            if(levelsCompleted <= ranking.rankRequirements)
            {
                return ranking.rankName;
            }
        }
        return null;
    }
}
