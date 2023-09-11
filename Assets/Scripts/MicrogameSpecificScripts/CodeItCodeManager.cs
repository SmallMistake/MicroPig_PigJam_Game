using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CodeItCodeManager : MicrogameGoalTracker
{
    [TextArea]
    public string[] codeBlocks;

    public TextMeshProUGUI codeArea;
    public Scrollbar scrollbar;

    private StringReader selectedCodeReader;
    void Start()
    {
        string selectedCodeString = SelectCodeBlock();
        selectedCodeReader = new StringReader(selectedCodeString);
        codeArea.text = "";

    }

    private void OnDestroy()
    {
        selectedCodeReader.Dispose();
    }

    private string SelectCodeBlock()
    {
        return codeBlocks[Random.Range(0, codeBlocks.Length)];
    }
    public void ShowNextCodeBlock()
    {
        string? firstLine = selectedCodeReader.ReadLine();
        if (firstLine != null)
        {
            codeArea.text += firstLine;
            codeArea.text += "\n";
            if(firstLine == "")
            {
                ShowNextCodeBlock();
            }
        }
        else
        {
            goalComplete = true;
            onGoalStatusChanged?.Invoke(true);
        }
    }
}
