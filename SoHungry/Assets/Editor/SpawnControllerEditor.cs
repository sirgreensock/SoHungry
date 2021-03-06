﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

[CustomEditor(typeof(SpawnController))]
public class SpawnControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SpawnController myScript = (SpawnController)target;
        if (GUILayout.Button("Start Spawner"))
        {
            myScript.SpawningAllowed = true;
        }
        if (GUILayout.Button("Stop Spawner"))
        {
            myScript.SpawningAllowed = false;
        }
    }
}