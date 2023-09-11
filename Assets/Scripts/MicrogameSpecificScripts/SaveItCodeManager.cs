using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SaveItCodeManager : MonoBehaviour
{
    public List<string> fileNames = new List<string>();
    public TextMeshProUGUI fileNameTextMesh;
    // Start is called before the first frame update
    void Start()
    {
        int selectedFileName = Random.Range(0, fileNames.Count);
        fileNameTextMesh.text = "Name: " + fileNames[selectedFileName];
    }
}
