using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class ObjectArea {
    public string areaName;
    public GameObject areaObject;
}


public class UIAreaManager : MonoBehaviour
{
    public string initialActiveArea;



    [SerializeField]
    List<ObjectArea> areas;


    private void OnEnable()
    {
        ChangeCurrentArea(initialActiveArea );
    }

    public void ChangeCurrentArea(string newArea)
    {
        foreach(ObjectArea area in areas)
        {
            if(area.areaName == newArea)
            {
                area.areaObject.SetActive(true);
            }
            else
            {
                area.areaObject.SetActive(false);
            }
        }
    }
}
