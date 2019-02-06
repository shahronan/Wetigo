/*
 *  Copyright 2013-Present Two Big Ears Limited
 *  All rights reserved.
 *  http://twobigears.com
 */

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

namespace TBE
{
    public class Setup : EditorWindow
    {
        public static Setup Instance { get; private set; }

        static Vector2 windowSize = new Vector2(300, 150);

        [MenuItem("Edit/Audio360/Setup")]
        static void Init()
        {
            // Get existing open window or if none, make a new one:     
            Instance = (Setup)EditorWindow.GetWindow(typeof(Setup));
            Instance.ShowUtility();
            GUIContent content = new GUIContent();
            content.text = "Setup";
            Instance.titleContent = content; 
            Instance.minSize = windowSize;
            Instance.maxSize = windowSize;
        }

        void OnGUI()
        {   
            EditorGUILayout.Space();

            GUILayout.Label("Audio360 — Project Setup", EditorStyles.boldLabel);
            if (GUILayout.Button("Setup Scene"))
            {
                createEngineObject();
            }
        }

        void createEngineObject()
        {
            string name = "[" + typeof(AudioEngineSettings).ToString() + "]";
            GameObject global = GameObject.Find(name);
            if (global != null)
            {
                GameObject.Destroy(global);
            }
           
            // Create a game object with components
            global = new GameObject(name);
            global.AddComponent<AudioEngineSettings>();
        }
    }
}
