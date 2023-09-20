using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoardController : MonoBehaviour
{
    public Transform scoresArea;
    public SaveSystemManager manager;
    public int numberOfRows = 10;
    public GameObject recordRowPrefab;

    [SerializeField]
    Animator endCanvasUIAnimator;

    [SerializeField]
    private float timeToWaitTillNewRecordCheck;

    private void OnEnable()
    {
        SaveSystemManager.OnDataSaved += FillScoresArea;

        if(manager == null)
        {
            manager = FindObjectOfType<SaveSystemManager>();
        }
        FillScoresArea();

        if (GameManager.Exists && IsNewRecord())
        {
            StartCoroutine(HandleNewRecordLogic());
        }
    }

    private void OnDisable()
    {
        SaveSystemManager.OnDataSaved -= FillScoresArea;
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

    IEnumerator HandleNewRecordLogic()
    {
        yield return new WaitForSeconds(timeToWaitTillNewRecordCheck);
        DisplayNewRecordArea();
    }

    private bool IsNewRecord()
    {
        if (GameManager.CompletedGames > FindObjectOfType<SaveSystemManager>().GetLowestRecord())
        {
            return true;
        }
        return false;
    }

    //TODO Replace the end canvas animator with a more generic one that for just the record area later;
    private void DisplayNewRecordArea()
    {
        if(endCanvasUIAnimator!= null)
        {
            endCanvasUIAnimator.SetTrigger("ShowNewRecordArea");
        };
    }

    //TODO same as above
    public void AcceptNewRecordName(string recordHolderName)
    {

        print("TODO Save Record: " + recordHolderName + "Levels: " + GameManager.CompletedGames);
        FindObjectOfType<SaveSystemManager>().SaveNewRecord(recordHolderName, GameManager.CompletedGames);
        if (endCanvasUIAnimator != null)
        {
            endCanvasUIAnimator.SetTrigger("HideNewRecordArea");
        }
    }
}
