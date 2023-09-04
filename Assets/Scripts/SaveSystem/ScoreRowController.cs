using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreRowController : MonoBehaviour
{
    public TextMeshProUGUI numberTextMesh;
    public TextMeshProUGUI nameTextMesh;
    public TextMeshProUGUI levelTextMesh;

    public void UpdateVisual(int number, RecordEntry recordEntry)
    {
        numberTextMesh.text = number.ToString();
        nameTextMesh.text = recordEntry.playerName;
        levelTextMesh.text = recordEntry.level.ToString();
    }
}
