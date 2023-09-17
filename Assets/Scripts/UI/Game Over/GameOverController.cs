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
    TextMeshProUGUI levelTextMesh;

    [SerializeField]
    Animator gameOverPanelManager;

    private void OnEnable()
    {
        HandleGameOver();
    }

    public void HandleGameOver()
    {
        int levelsCompleted = GetLevelsCompleted();
        levelTextMesh.text = levelsCompleted.ToString();
        string ranking = GetRanking(levelsCompleted);
    }

    private int GetLevelsCompleted()
    {
        return GameManager.CompletedGames;
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
