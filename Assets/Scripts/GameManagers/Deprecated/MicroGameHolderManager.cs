using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicroGameHolderManager : MonoBehaviour
{
    public GameObject microgameHUD;

    private GameObject microGameInstance;

    public MicrogameManager LoadMicroGame(Microgame microgame)
    {
        microGameInstance = Instantiate(microgame.microgamePrefab);
        microGameInstance.SetActive(false);
        microGameInstance.transform.SetParent(transform);
        return microGameInstance.GetComponentInChildren<MicrogameManager>();
    }
    public void TransitionToMicroGame()
    {
        CreateMicrogameHUD();
        microGameInstance.SetActive(true);
    }

    private void CreateMicrogameHUD()
    {
        GameObject microgameHUDInstance = Instantiate(microgameHUD);
        microgameHUDInstance.transform.SetParent(transform);
    }

    public void HandleMicroGameEnd()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        microGameInstance = null;
    }
}
