using UnityEditor;
using UnityEngine;

namespace pl.EpicVR.Interaction
{
    [CustomEditor(typeof(HandButton))]
    public class HandButtonEditor : Editor
    {
        SerializedProperty interactionManagerProperty;
        SerializedProperty layerMaskProperty;
        SerializedProperty onPressEventProperty;

        private void OnEnable()
        {
            interactionManagerProperty = serializedObject.FindProperty("interactionManager");
            layerMaskProperty = serializedObject.FindProperty("interactionLayerMask");
            onPressEventProperty = serializedObject.FindProperty("OnPress");
        }


        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(interactionManagerProperty, new GUIContent("Interaction Manager"));
            EditorGUILayout.PropertyField(layerMaskProperty, new GUIContent("Interaction with LayerMask"));

            GUILayout.Space(20);
            EditorGUILayout.PropertyField(onPressEventProperty, new GUIContent("OnPress Event"));

            serializedObject.ApplyModifiedProperties();
        }
    }
}