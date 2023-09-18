using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterEffectsController : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRendererToCopy;

    [SerializeField]
    private GameObject objectToSpawn;

    [SerializeField]
    private float timeBetweenSpawns;

    [SerializeField]
    private float timeTillAfterEffectDeath;

    [SerializeField]
    private Color afterEffectColor;

    private Coroutine spawnerCoroutine;

    private void OnEnable()
    {
        spawnerCoroutine = StartCoroutine(SpawnerCoroutine());
    }

    private void OnDisable()
    {
        StopCoroutine(spawnerCoroutine);
        spawnerCoroutine = null;
    }

    IEnumerator SpawnerCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenSpawns);
            GameObject gameObject = Instantiate(objectToSpawn);
            gameObject.GetComponent<SpriteRenderer>().sprite = spriteRendererToCopy.sprite;
            gameObject.GetComponent<SpriteRenderer>().color = afterEffectColor;
            gameObject.transform.position = spriteRendererToCopy.transform.position;
            gameObject.transform.rotation = spriteRendererToCopy.transform.rotation;
            gameObject.GetComponent<DestroyAfterTime>().RestartCountdown(timeTillAfterEffectDeath);
        }
    }


}
