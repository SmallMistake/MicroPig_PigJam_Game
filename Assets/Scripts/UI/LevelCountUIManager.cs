using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelCountUIManager : MonoBehaviour
{
    public TextMeshProUGUI currentLevelTextMesh;
    // Start is called before the first frame update
    void Awake()
    {
        GameManager.OnLevelSuccess += HandleLevelIncrease;
        currentLevelTextMesh.text = "0";
    }

    private void OnDestroy()
    {
        GameManager.OnLevelSuccess -= HandleLevelIncrease;
    }

    private void HandleLevelIncrease(int currentLevel)
    {
        currentLevelTextMesh.text = currentLevel.ToString();
    }
}
