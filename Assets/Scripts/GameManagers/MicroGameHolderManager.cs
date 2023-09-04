using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicroGameHolderManager : MonoBehaviour
{
    public GameObject placeholderMicroGame;

    private GameObject microGameInstance;
    public void LoadMicroGame(Microgame microgame)
    {
        microGameInstance = Instantiate(microgame.microgamePrefab);
        microGameInstance.SetActive(false);
    }
    public void TransitionToMicroGame()
    {
        microGameInstance.SetActive(true);
    }

    public void HandleMicroGameEnd()
    {
        Destroy(microGameInstance);
    }
}
