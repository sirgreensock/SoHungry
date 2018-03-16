using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class GameManagerDebug : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GameManager myScript = (GameManager)target;
        if (GUILayout.Button("StartGame"))
        {
           myScript.StartGame();
        }
        if (GUILayout.Button("StopGame"))
        {
            myScript.EndGame();
        }
    }
}