using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    public GameObject initialPage;
    public List<GameObject> additionalPages;

    private GameObject currentMenu;

    void Awake()
    {
        initialPage.SetActive(true);
        foreach (var page in additionalPages)
        {
            page.SetActive(false);
        }
        currentMenu = initialPage;
    }

    public void ChangePage(GameObject nextPage)
    {

        currentMenu.SetActive(false);
        currentMenu = nextPage;
        currentMenu.SetActive(true);
    }
}
