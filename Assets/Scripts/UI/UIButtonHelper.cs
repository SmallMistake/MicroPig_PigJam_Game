using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButtonHelper : MonoBehaviour
{
    [SerializeField]
    private Button buttonBeingListenedTo; //Setting this will make sure that sounds are only played when approroriate
    private string hoverSound = "event:/SFX/UI/Hover";
    private string selectSound = "event:/SFX/UI/Select";


    private void Start()
    {
        if(buttonBeingListenedTo == null)
        {
            buttonBeingListenedTo = GetComponent<Button>();
        }
    }


    public void PlayHoverSound()
    {
        if(EventSystem.current.currentSelectedGameObject != buttonBeingListenedTo.gameObject)
        {
            FMODUnity.RuntimeManager.PlayOneShot(hoverSound);
        }
    }

    public void PlaySelectSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(selectSound);
    }
}
