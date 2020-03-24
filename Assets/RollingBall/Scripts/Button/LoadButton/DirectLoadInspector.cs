using UnityEditor;

[CustomEditor(typeof(LoadButton))]
public sealed class DirectLoadInspector : Editor
{
    public override void OnInspectorGUI()
    {
        var loadButton = target as LoadButton;

        serializedObject.Update();

        DrawDefaultInspector();

        if (loadButton != null && loadButton.GetLoadType() == LoadType.Direct)
        {
            loadButton.stageNumber = EditorGUILayout.IntSlider("Stage Number", loadButton.stageNumber, 0, 9);
        }

        serializedObject.ApplyModifiedProperties();
    }
}