using UnityEditor;
using UnityEditor.XR.Interaction.Toolkit;

[CustomEditor(typeof(SocketTag))]
public class SocketTagEditor : XRSocketInteractorEditor
{
    SerializedProperty targetTag;

    protected new void OnEnable()
    {
        targetTag = serializedObject.FindProperty("targetTag");
        base.OnEnable();
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(targetTag);
        serializedObject.ApplyModifiedProperties();

        base.OnInspectorGUI();
    }
}
