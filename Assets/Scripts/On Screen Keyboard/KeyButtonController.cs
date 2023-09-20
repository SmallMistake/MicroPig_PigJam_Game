using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyButtonController : MonoBehaviour
{
    public OnscreenKeyboardController keyboardController;
    public string overridingValue;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => { 
            if(overridingValue != null && overridingValue == "")
            {
                keyboardController.KeyPressed(GetComponentInChildren<TextMeshProUGUI>().text);
            }
            else
            {
                keyboardController.KeyPressed(overridingValue);
            }
        });
    }
}
