using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(MicrogameManager), true)]
public class MicrogameManagerEditor : Editor
{
    public MicrogameManager microgameManagerTarget
    {
        get
        {
            return (MicrogameManager)target;
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
        if (microgameManagerTarget == null)
        {
            return;
        }

        // we add a button to manually empty the inventory
        EditorGUILayout.Space();
        if (GUILayout.Button("Add Timer"))
        {
            microgameManagerTarget.AddComponent<MicrogameTimer>();

            Debug.Log("Be sure to add the Time Out function to the timer");
            //microgameManagerTarget.SpawnParticles();
            SceneView.RepaintAll();
        }

        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
            SceneView.RepaintAll();
        }

        // we apply our changes
        serializedObject.ApplyModifiedProperties();
    }
}
