using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameplayStates { LevelAnouncementScreen, GameplayScreen} 
public class MainGameplayManager : MonoBehaviour
{
    public int currentLevel;
    public GameplayStates currentState;
    public List<Customer> unlockedCustomers;
    public CustomerManager customerManager;
    public MicroGameHolderManager microGameHolderManager;


    public void HandleResults()
    {
        currentLevel++;
        SetupNextCustomer();
    }


    public void SetupNextCustomer()
    {
        customerManager.SetCustomer(ChooseCustomer());
    }

    private Customer ChooseCustomer()
    {
        int selectedCustomerIndex = Random.Range(0, unlockedCustomers.Count);
        return unlockedCustomers[selectedCustomerIndex];
    }

    public void SetupMicrogamePhase()
    {
        Microgame microgame = customerManager.GetMiniGame();
        microGameHolderManager.LoadMicroGame(microgame);
        microGameHolderManager.TransitionToMicroGame();
    }


    public void SetCurrentState(GameplayStates newState)
    {
        currentState = newState;
    }
}
