using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomerManager : MonoBehaviour
{
    private Customer currentCustomer;

    public Image customerImage;
    public TextMeshProUGUI customerNameTextMesh;
    public TextMeshProUGUI customerDescriptionTextMesh;

    public void SetCustomer(Customer newCustomer)
    {
        currentCustomer = newCustomer;
        EndCustomerInteraction();
        StartCustomerInteraction(currentCustomer);
    }

    private void StartCustomerInteraction(Customer customer)
    {
        customerNameTextMesh.text = customer.name;
        customerDescriptionTextMesh.text = customer.gameType;
        Sprite customerSprite = customer.sprites[Random.Range(0, customer.sprites.Count)];
        customerImage.sprite = customerSprite;
    }

    private void EndCustomerInteraction()
    {
        customerNameTextMesh.text = "";
        customerDescriptionTextMesh.text = "";
        customerImage.sprite = null;
    }

    public string GetMiniGame()
    {
        int minigameIndex = Random.Range(0, currentCustomer.microGameScenePaths.Count);
        return currentCustomer.microGameScenePaths[minigameIndex];
    }
}
