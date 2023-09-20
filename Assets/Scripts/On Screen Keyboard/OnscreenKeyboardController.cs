using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OnscreenKeyboardController : MonoBehaviour
{
    public GameObject keyButton;
    public GameObject keyboardRow;
    public List<GameObject> specialRows;
    public int numberOfKeysPerRow;
    public List<string> primaryKeyboard = new List<string>{
        "a",
        "b",
        "c",
        "d",
        "e",
        "f",
        "g",
        "h",
        "i",
        "j",
        "k",
        "l",
        "m",
        "n",
        "o",
        "p",
        "q",
        "r",
        "s",
        "t",
        "u",
        "v",
        "w",
        "x",
        "y",
        "z",
        "à",
        "â",
        "ä",
        "æ",
        "ç",
        "è",
        "é",
        "ê",
        "ë",
        "ì",
        "ï",
        "đ",
        "ñ",
        "ò",
        "ô",
        "ö",
        "œ",
        "ù",
        "û",
        "ü",
        "'",
        "-",
        ":",
        "!",
        "?",
        "0",
        "1",
        "2",
        "3",
        "4",
        "5",
        "6",
        "7",
        "8",
        "9",
        ",",
        "."
    };
    public List<string> secondaryKeyboard = new List<string>
    {
        "A",
        "B",
        "C",
        "D",
        "E",
        "F",
        "G",
        "H",
        "I",
        "J",
        "K",
        "L",
        "M",
        "N",
        "O",
        "P",
        "Q",
        "R",
        "S",
        "T",
        "U",
        "V",
        "W",
        "X",
        "Y",
        "Z",
        "À",
        "Â",
        "Ä",
        "Æ",
        "Ç",
        "È",
        "É",
        "Ì",
        "Î",
        "Ï",
        "Ð",
        "Ñ",
        "Ò",
        "Ô",
        "Ö",
        "Œ",
        "Ù",
        "Û",
        "Ü",
        "'",
        "-",
        ":",
        "!",
        "?",
        "0",
        "1",
        "2",
        "3",
        "4",
        "5",
        "6",
        "7",
        "8",
        "9",
        ",",
        "."
    };

    public UnityEvent<string> textSubmitted;
    private GameObject currentRow;

    public List<TextDestinationController> textDestinationControllers = new List<TextDestinationController>();

    /// <summary>
    /// Creates and sets up the inventory display (usually called via the inspector's dedicated button)
    /// </summary>
    public virtual void SetupKeyboardDisplay()
    {
        List<string> tempList = primaryKeyboard;

        int currentRowIndex = 0;

        foreach (string letter in tempList)
        {
            if(currentRowIndex == 0)
            {
                currentRow = Instantiate(keyboardRow);
                currentRow.transform.SetParent(gameObject.transform);
                currentRow.transform.localScale = Vector3.one;
            }
            currentRowIndex++;

            GameObject keyButtonInstance = Instantiate(keyButton);
            keyButtonInstance.GetComponentInChildren<TextMeshProUGUI>().text = letter;
            keyButtonInstance.transform.SetParent(currentRow.transform);
            keyButtonInstance.transform.localScale = Vector3.one;
            keyButtonInstance.GetComponent<KeyButtonController>().keyboardController = this;

            if (currentRowIndex >= numberOfKeysPerRow) {
                currentRowIndex = 0;
            }
        }

        foreach (GameObject specialRow in specialRows)
        {
            GameObject specialRowInstance = Instantiate(specialRow);
            foreach(KeyButtonController button in specialRowInstance.GetComponentsInChildren<KeyButtonController>())
            {
                button.keyboardController = this;
            }
            specialRowInstance.transform.SetParent(gameObject.transform);
            specialRowInstance.transform.localScale = Vector3.one;
        }
    }

    public void KeyPressed(string key)
    {
        foreach(TextDestinationController textDestinationController in textDestinationControllers)
        {
            if(key.ToLower() == "backspace")
            {
                textDestinationController.RemoveCharacter();
            }
            else if(key.ToLower() == "submit")
            {
                textSubmitted.Invoke(textDestinationController.GetText());
            }
            else
            {
                textDestinationController.AddCharacter(key);
            }
        }
    }
}
