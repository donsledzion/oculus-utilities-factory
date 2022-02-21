using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CameraFade))]
public class CameraFadeEditor : Editor
{
    private CameraFade cameraFade;

    private void OnEnable()
    {
        cameraFade = (CameraFade)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Space(20);
        GUI.enabled = Application.isPlaying;
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("FadeIn")) cameraFade.CurrentFadeType = CameraFade.FadeType.FadeIn;
        if (GUILayout.Button("FadeOut")) cameraFade.CurrentFadeType = CameraFade.FadeType.FadeOut;
        EditorGUILayout.EndHorizontal();
        GUI.enabled = true;
    }
}
