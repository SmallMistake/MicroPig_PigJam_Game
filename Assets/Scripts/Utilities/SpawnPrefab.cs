using PixelCrushers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPrefab : MonoBehaviour
{
    public bool parentToObject = false;
    public bool parentToParent = false;
    public Vector3 offset; //TODO if Needed

    public void SpawnObject(GameObject objectToSpawn)
    {
        GameObject spawnedObject = Instantiate(objectToSpawn);
        if(parentToParent)
        {
            GameObject canvas = GetComponentInParent<Canvas>().gameObject;
            spawnedObject.transform.SetParent(gameObject.transform.parent.transform);
        }
        spawnedObject.transform.position = offset;
        spawnedObject.transform.position = transform.position;
        spawnedObject.transform.localScale = Vector3.one;
    }
}
