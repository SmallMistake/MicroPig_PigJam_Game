using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class FluidSpawnLocation : MonoBehaviour
{
    public Vector2 numberToSpawn = new Vector2(5,5);

    public GameObject particlesToSpawn;

    public float particleRadius = 0.5f;

    private GameObject waterHolder;
    


    public void SpawnParticles()
    {
        if(waterHolder == null)
        {
            waterHolder = Instantiate(new GameObject("Water Holder"));
        }
        Vector3 spawnTopLeft = transform.position - new Vector3(transform.localScale.x, transform.localScale.y, transform.position.z) / 2;
        for (int x = 0; x < numberToSpawn.x; x++)
        {
            for (int y = 0; y < numberToSpawn.y; y++)
            {
                Vector3 spawnPosition = spawnTopLeft + new Vector3(x * particleRadius * 2, y * particleRadius * 2, 0) + Random.onUnitSphere * particleRadius * 0.1f;
                particlesToSpawn.transform.position = spawnPosition;
                GameObject spawnedParticle = Instantiate(particlesToSpawn);
                spawnedParticle.transform.parent = waterHolder.transform;

            }
        }
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            //Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
            //Gizmos.matrix = rotationMatrix;
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(transform.position,new Vector2(transform.localScale.x, transform.localScale.y));
        }
    }
}
