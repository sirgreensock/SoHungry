using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class GameManagerDebug : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GameManager myScript = (GameManager)target;
        if (GUILayout.Button("TestButton"))
        {
           // myScript.ChooseSpawn();
        }
    }
}