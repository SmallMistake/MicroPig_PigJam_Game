using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


//Used by things like the onscreen keyboard to be able to handle key presses
public class TextDestinationController : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public int maxCharacterLength = 12;

    public void AddCharacter(string character) { 
        if(textMesh.text.Length < maxCharacterLength)
        {
            textMesh.text += character;
        }
    }

    public void RemoveCharacter()
    {
        if(textMesh.text.Length > 0)
        {
            textMesh.text = textMesh.text.Remove(textMesh.text.Length - 1, 1);
        }
    }

    public string GetText()
    {
        return textMesh.text;
    }
}
