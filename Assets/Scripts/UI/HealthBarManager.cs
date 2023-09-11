using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarManager : MonoBehaviour
{
    private void Awake()
    {
        GameManager.OnHealthChanged += UpdateHealthBar;
    }

    private void OnDestroy()
    {
        GameManager.OnHealthChanged -= UpdateHealthBar;
    }

    public List<GameObject> listOfHealthVisuals;
    public void SetupHealthBar(int totalOfHealthPoints)
    {
        //TODO Later
    }

    public void UpdateHealthBar(int currentOfHealthPoints)
    {
        for(int i = 0; i < listOfHealthVisuals.Count; i++)
        {
            if(i < currentOfHealthPoints)
            {
                listOfHealthVisuals[i].SetActive(true);
            }
            else
            {
                listOfHealthVisuals[i].SetActive(false);
            }
        }
    }
}
