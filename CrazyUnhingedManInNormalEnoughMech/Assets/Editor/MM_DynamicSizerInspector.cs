using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MM_DynamicSizer))]
public class MM_DynamicSizerInspecter : Editor
{
    SerializedProperty DynamicWidth;
    SerializedProperty DynamicHeight;
    SerializedProperty xPercent;
    SerializedProperty yPercent;
    SerializedProperty Width;
    SerializedProperty Height;
    private void OnEnable()
    {
        DynamicWidth = serializedObject.FindProperty("m_dynamicWidth");
        DynamicHeight = serializedObject.FindProperty("m_dynamicHeight");
        xPercent = serializedObject.FindProperty("m_xPercent");
        yPercent = serializedObject.FindProperty("m_yPercent");
        Width = serializedObject.FindProperty("m_width");
        Height = serializedObject.FindProperty("m_height");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        Labels(100);

        BeginHorizontal();

        if (DynamicWidth.boolValue)
            GUIDisable();
        Property(Width, 50, 100);

        GUIEnable();

        Property(DynamicWidth,"X",15);

        if (DynamicWidth.boolValue)
            PropSlider(xPercent, 0, 100);

        EndHorizontal();

        BeginHorizontal();

        if (DynamicHeight.boolValue)
            GUIDisable();
        Property(Height, 50, 100);

        GUIEnable();

        Property(DynamicHeight, "Y", 15);

        if (DynamicHeight.boolValue)
        {
            PropSlider(yPercent, 0, 100);
        }

        EndHorizontal();

        ApplyProperties();
    }

    #region Wrappers
    private void ApplyProperties()
    {
        serializedObject.ApplyModifiedProperties();
    }
    private void PropSlider(SerializedProperty prop, float min, float max)
    {
        prop.floatValue = EditorGUILayout.Slider(prop.floatValue, min, max);
    }

    private void Labels(float size)
    {
        EditorGUIUtility.labelWidth = size;
    }

    private void GUIDisable()
    {
        GUI.enabled = false;
    }

    private void GUIEnable()
    {
        GUI.enabled = true;
    }

    private void Property(SerializedProperty prop)
    {
        EditorGUILayout.PropertyField(prop, GUILayout.Width(130));
    }

    private void Property(SerializedProperty prop, float labelWidth)
    {
        float temp = EditorGUIUtility.labelWidth;
        EditorGUIUtility.labelWidth = labelWidth;
        EditorGUILayout.PropertyField(prop);
        EditorGUIUtility.labelWidth = temp;
    }

    private void Property(SerializedProperty prop, float labelWidth, float fieldWidth)
    {
        float temp = EditorGUIUtility.labelWidth;
        EditorGUIUtility.labelWidth = labelWidth;
        EditorGUILayout.PropertyField(prop, GUILayout.Width(fieldWidth));
        EditorGUIUtility.labelWidth = temp;
    }

    private void Property(SerializedProperty prop, string label, float labelWidth, float fieldWidth)
    {
        float temp = EditorGUIUtility.labelWidth;
        EditorGUIUtility.labelWidth = labelWidth;
        EditorGUILayout.PropertyField(prop, new GUIContent(label), GUILayout.Width(fieldWidth));
        EditorGUIUtility.labelWidth = temp;
    }

    private void Property(SerializedProperty prop, string label, float labelWidth)
    {
        float temp = EditorGUIUtility.labelWidth;
        EditorGUIUtility.labelWidth = labelWidth;
        EditorGUILayout.PropertyField(prop, new GUIContent(label));
        EditorGUIUtility.labelWidth = temp;
    }
    private void BeginHorizontal()
    {
        EditorGUILayout.BeginHorizontal();
    }

    private void EndHorizontal()
    {
        EditorGUILayout.EndHorizontal();
    }

    private void Space(float amount)
    {
        EditorGUILayout.Space(amount);
    }

    #endregion
}