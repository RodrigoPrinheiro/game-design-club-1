using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEditor;

[CustomEditor(typeof(PoemGenerator))]
public class PoemCustomEditor : Editor
{
    public SerializedProperty poem;
    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        poem = serializedObject.FindProperty("poem");
    }
    
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(poem);
        if (GUILayout.Button("New Poem"))
        {
            (target as PoemGenerator).SetNewPoem();
            serializedObject.ApplyModifiedProperties();
        }
    }

    protected override void OnHeaderGUI()
    {
        
    }
}
