using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetToMousePosition : MonoBehaviour
{
    public bool enabled = true;
    public bool hideDefaultCursor;
    // Start is called before the first frame update
    private void OnEnable()
    {
        Cursor.visible = false;
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            Cursor.visible = false;
        }
        else
        {
            //Cursor.visible = false;
        }
    }

    private void OnDisable()
    {
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (enabled)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(worldPosition.x, worldPosition.y, 0);
        }
    }
}
