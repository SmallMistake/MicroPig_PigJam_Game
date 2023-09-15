using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DebugHelper : MonoBehaviour
{
    GameManager gameManager;

    private bool VerifyGameManager()
    {
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return true;
        }

    }

    public void KillPlayer()
    {
        if (VerifyGameManager())
        {
            gameManager.IncreaseLosses(990);
        }
    }
}
