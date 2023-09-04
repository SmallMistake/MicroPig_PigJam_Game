using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoardController : MonoBehaviour
{
    public Transform scoresArea;
    public SaveSystemManager manager;
    public int numberOfRows = 10;
    public GameObject recordRowPrefab;

    private void OnEnable()
    {
        FillScoresArea();
    }

    public void FillScoresArea()
    {
        List<RecordEntry> records = manager.GetRecords();

        foreach (Transform child in scoresArea.transform) { Destroy(child.gameObject); } //Clear out previous records if exist

        int currentRowCount = 0;
        foreach (RecordEntry record in records) //Create new records
        {
            if (currentRowCount < numberOfRows)
            {
                GameObject recordEntry = Instantiate(recordRowPrefab);
                recordEntry.transform.SetParent(scoresArea.transform);
                recordEntry.GetComponent<ScoreRowController>().UpdateVisual(currentRowCount + 1, record);
                currentRowCount++;
            }
        }
        while (currentRowCount < numberOfRows)
        {
            GameObject recordEntry = Instantiate(recordRowPrefab);
            recordEntry.transform.SetParent(scoresArea.transform);
            recordEntry.GetComponent<ScoreRowController>().UpdateVisual(currentRowCount + 1, new RecordEntry());
            currentRowCount++;
        }
    }
}
