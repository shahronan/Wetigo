/*
 *  Copyright 2013-Present Two Big Ears Limited
 *  All rights reserved.
 *  http://twobigears.com
 */

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace TBE
{
    [CustomEditor(typeof (AudioEngineSettings))]
    public class AudioEngineSettingsEditor : Editor
    {
        SerializedProperty sampleRate;
        SerializedProperty objectPoolSize;
        SerializedProperty audioDevice;
        SerializedProperty customAudioDevice;

        void OnEnable()
        {
            sampleRate = serializedObject.FindProperty("sampleRate");
            objectPoolSize = serializedObject.FindProperty("objectPoolSize");
            audioDevice = serializedObject.FindProperty("audioDevice");
            customAudioDevice = serializedObject.FindProperty("customAudioDevice");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            AudioEngineSettings man = (AudioEngineSettings) target;

            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(sampleRate, new GUIContent("Sample Rate"));
            EditorGUILayout.PropertyField(objectPoolSize, new GUIContent("Object Pool Size"));
            EditorGUILayout.PropertyField(audioDevice, new GUIContent("Audio Device"));
            EditorGUI.BeginDisabledGroup(man.audioDevice != AudioDeviceType.CUSTOM);

            EditorGUI.indentLevel = 1;
            EditorGUILayout.PropertyField(customAudioDevice, new GUIContent("Custom Device"));
            EditorGUI.indentLevel = 0;
            EditorGUI.EndDisabledGroup();

            serializedObject.ApplyModifiedProperties();
        }

    }
}