using UnityEngine;
using UnityEditor;

namespace IntronDigital
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(SaveSystemManager), true)]
    /// <summary>
    /// Custom editor for the InventoryDisplay component
    /// </summary>
    public class SaveSystemManagerEditor : Editor
    {
        /// <summary>
        /// Gets the target inventory component.
        /// </summary>
        /// <value>The inventory target.</value>
        public SaveSystemManager saveSystemManagerTarget
        {
            get
            {
                return (SaveSystemManager)target;
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
            if (saveSystemManagerTarget == null)
            {
                return;
            }

            // we add a button to manually empty the inventory
            EditorGUILayout.Space();
            if (GUILayout.Button("Save Records to File"))
            {
                saveSystemManagerTarget.SaveRecords();
                SceneView.RepaintAll();
            }

            // we add a button to manually empty the inventory
            EditorGUILayout.Space();
            if (GUILayout.Button("Read Records from File"))
            {
                saveSystemManagerTarget.ReadRecords();
                SceneView.RepaintAll();
            }

            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
                SceneView.RepaintAll();
            }

            // we apply our changes
            serializedObject.ApplyModifiedProperties();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
        }
    }
}