using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]

public class ColorCombo {
    public string colorName;
    public Color color;
}

public class PowerItCodeManager : MonoBehaviour
{
    [SerializeField]
    List<ColorCombo> colorList = new List<ColorCombo>(); //Used to pick colors that will need to be matched.

    [SerializeField]
    List<GameObject> cords;

    [SerializeField]
    List<GameObject> outlets;

    [SerializeField]
    int numberOfColorsToPick = 2; // The number of colors that will need to be matched

    [SerializeField]
    MicrogameManager microgameManager; //Set the goals
    
    // Start is called before the first frame update
    void Awake()
    {
        for(int i = 0; i < numberOfColorsToPick; i++)
        {
            ColorCombo colorCombo = PickColor();
            ColorObjects(colorCombo);
        }
    }

    private ColorCombo PickColor()
    {
        int colorComboIndex = UnityEngine.Random.Range(0, colorList.Count);
        ColorCombo selectedColorCombo = colorList[colorComboIndex];
        colorList.RemoveAt(colorComboIndex);
        return selectedColorCombo;
    }

    private void ColorObjects(ColorCombo colorCombo) {
        // Color Cord
        GameObject cord = cords[0];
        cords.RemoveAt(0);
        cord.GetComponent<Draggable>().itemName = colorCombo.colorName;
        cord.GetComponentInChildren<SpriteRenderer>().color = colorCombo.color;

        // Color Outlet

        int randomOutletIndex = UnityEngine.Random.Range(0, outlets.Count);
        GameObject outlet = outlets[randomOutletIndex];
        outlets.RemoveAt(randomOutletIndex);
        outlet.GetComponent<DropHolderFilledGoal>().acceptedItemNames.Add(colorCombo.colorName);
        outlet.GetComponent<SpriteRenderer>().color = colorCombo.color;

        microgameManager.microgameGoals.Add(outlet.GetComponent<DropHolderFilledGoal>());
    }

}
