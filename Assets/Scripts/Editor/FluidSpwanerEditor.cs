using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UI;

namespace IntronDigital
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(FluidSpawnLocation), true)]
    /// <summary>
    /// Custom editor for the InventoryDisplay component
    /// </summary>
    public class FluidSpawnLocationEditor : Editor
    {
        /// <summary>
        /// Gets the target inventory component.
        /// </summary>
        /// <value>The inventory target.</value>
        public FluidSpawnLocation fluidSpawnLocationTarget
        {
            get
            {
                return (FluidSpawnLocation)target;
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
            if (fluidSpawnLocationTarget == null)
            {
                return;
            }

            // we add a button to manually empty the inventory
            EditorGUILayout.Space();
            if (GUILayout.Button("Spawn Water Particles"))
            {
                fluidSpawnLocationTarget.SpawnParticles();
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