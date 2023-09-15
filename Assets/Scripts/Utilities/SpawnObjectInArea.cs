using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectInArea : MonoBehaviour
{
    public GameObject gameObjectToSpawn;
    public Collider2D spawnArea;
    
    public void SpawnObject(int numberToSpawn)
    {
        for(int i = 0; i < numberToSpawn; i++) {
            GameObject gameObject = Instantiate(gameObjectToSpawn);
            gameObject.transform.position = PickARandomPointArea(spawnArea.bounds);
        }

    }

    private Vector3 PickARandomPointArea(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }
}
