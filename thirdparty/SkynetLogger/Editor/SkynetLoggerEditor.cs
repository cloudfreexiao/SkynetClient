﻿using UnityEngine;
using UnityEditor;

public class SkynetLoggerEditor : EditorWindow
{
    [MenuItem("Skynet/Logger Window")]
    public static void ShowWindow()
    {
        GetWindow(typeof(SkynetLoggerEditor));
    }

    [SerializeField]
    private Channel loggerChannels = SkynetLogger.kAllChannels;

    private void OnGUI()
    {
        EditorGUI.BeginChangeCheck();
 
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Clear all"))
        {
            loggerChannels = 0;
        }
        if (GUILayout.Button("Select all"))
        {
            loggerChannels = SkynetLogger.kAllChannels;
        }

        EditorGUILayout.EndHorizontal();

        GUILayout.Label("Click to toggle logging channels", EditorStyles.boldLabel);
        
        foreach (Channel channel in System.Enum.GetValues(typeof(Channel)))
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Toggle((loggerChannels & channel) == channel, "", GUILayout.ExpandWidth(false));
            if (GUILayout.Button(channel.ToString()))
            {
                loggerChannels ^= channel;
            }
            EditorGUILayout.EndHorizontal();
        }

        // If the game is playing then update it live when changes are made
        if (EditorApplication.isPlaying && EditorGUI.EndChangeCheck())
        {
            SkynetLogger.SetChannels(loggerChannels);
        }
    }
    
    // When the game starts update the logger instance with the users selections
    private void OnEnable()
    {
        SkynetLogger.SetChannels(loggerChannels);
    }
}
