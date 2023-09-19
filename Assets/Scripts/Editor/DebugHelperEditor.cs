using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(DebugHelper), true)]
/// <summary>
/// Custom editor for the InventoryDisplay component
/// </summary>
public class DebugHelperEditor : Editor
{
    public DebugHelper debugHelperTarget
    {
        get
        {
            return (DebugHelper)target;
        }
    }


    /// <summary>
    /// Custom editor for the inventory panel.
    /// </summary>
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUI.BeginChangeCheck();
        // if there's a change in the inspector, we resize our inventory and grid, and redraw the whole thing.

        Editor.DrawPropertiesExcluding(serializedObject, new string[] { });

        // if for some reason we don't have a target inventory, we do nothing and exit
        if (debugHelperTarget == null)
        {
            return;
        }

        // we add a button to manually empty the inventory
        EditorGUILayout.Space();
        if (GUILayout.Button("Kill Player"))
        {
            debugHelperTarget.KillPlayer();
            SceneView.RepaintAll();
        }

        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
            SceneView.RepaintAll();
        }
    }
}
