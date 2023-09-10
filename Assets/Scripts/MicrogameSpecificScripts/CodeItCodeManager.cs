using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CodeItCodeManager : MicrogameGoalTracker
{
    [TextAreaAttribute]
    public List<string> codeBlocks;

    public TextMeshProUGUI codeArea;
    public ScrollRect scrollArea;

    private StringReader selectedCodeReader;
    void Start()
    {
        string selectedCodeString = SelectCodeBlock();
        selectedCodeReader = new StringReader(selectedCodeString);

    }

    private void OnDestroy()
    {
        selectedCodeReader.Dispose();
    }

    private string SelectCodeBlock()
    {
        return codeBlocks[Random.Range(0, codeBlocks.Count)];
    }
    public void ShowNextCodeBlock()
    {
        string? firstLine = selectedCodeReader.ReadLine();
        if (firstLine != null)
        {
            codeArea.text += firstLine;
            codeArea.text += "\n";
            ///scrollArea.val
        }
        else
        {
            onGoalStatusChanged?.Invoke(true);
        }
    }
}
