using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuPageManager : MonoBehaviour
{
    //Goes through list till finds active button
    public List<GameObject> firstSelectedButton;

    private GameObject previouslySelectedButton;

    bool firstEnable;

    private void OnEnable()
    {
        firstEnable = true;
    }

    private void Update()
    {
        if (firstEnable)
        {
            firstEnable = false;
            if (previouslySelectedButton != null)
            {
                EventSystem.current.SetSelectedGameObject(previouslySelectedButton);
            }
            else
            {
                foreach (GameObject button in firstSelectedButton)
                {
                    if (button.gameObject.activeInHierarchy)
                    {
                        EventSystem.current.SetSelectedGameObject(button.gameObject);
                        break;
                    }
                }
            }
        }
        if(EventSystem.current.currentSelectedGameObject != null)
        {
            previouslySelectedButton = EventSystem.current.currentSelectedGameObject;
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(previouslySelectedButton);
        }
        if (Input.GetButtonDown("Primary"))
        //if (Input.GetButtonDown("Select") || Input.GetButtonDown("Start") || Input.GetButtonDown("Primary"))
        {
            EventSystem.current.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
        }
    }
}
