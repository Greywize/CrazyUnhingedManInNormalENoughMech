using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MM_LeaderboardController))]
public class MM_LeaderboardControllerInspector : Editor
{
    SerializedProperty Name;
    SerializedProperty Placement;
    SerializedProperty Score;
    SerializedProperty MaxEntries;
    
    SerializedProperty EntryList;

    SerializedProperty EntryContainer;
    SerializedProperty EntryPrefab;

    private void OnEnable()
    {
        Name = serializedObject.FindProperty("testName");
        Placement = serializedObject.FindProperty("testPlacement");
        Score = serializedObject.FindProperty("testScore");
        MaxEntries = serializedObject.FindProperty("maxEntries");

        EntryList = serializedObject.FindProperty("entryList");

        EntryContainer = serializedObject.FindProperty("entryContainer");
        EntryPrefab = serializedObject.FindProperty("entryPrefab");
    }
    public override void OnInspectorGUI()
    {
        MM_LeaderboardController @object = (MM_LeaderboardController)target;

        LabelBold("Create Entry");

        IncreaseIndent();

            Property(Name);
            Property(Placement);
            Property(Score);

        DecreaseIndent();

        BeginHorizontal();
        if (GUILayout.Button("Clear"))
        {

        }
        if (GUILayout.Button("Add"))
        {

        }
        EndHorizontal();
        if (GUILayout.Button("Sort"))
        {

        }

        Space(20);

        // Leaderboard options
        LabelBold("Leaderboard");

        IncreaseIndent();

            Property(MaxEntries);
            Property(EntryList);

        DecreaseIndent();

        Space(20);

        Property(EntryContainer);
        Property(EntryPrefab);

        // Finally, apply properties
        ApplyProperties();
    }

    #region Wrappers
    private void Property(SerializedProperty property)
    {
        EditorGUILayout.PropertyField(property);
    }
    private void Label(string label)
    {
        EditorGUILayout.LabelField(label);
    }
    private void LabelBold(string label)
    {
        EditorGUILayout.LabelField(label, EditorStyles.boldLabel);
    }
    private void BeginHorizontal()
    {
        EditorGUILayout.BeginHorizontal();
    }
    private void EndHorizontal()
    {
        EditorGUILayout.EndHorizontal();
    }
    private void IncreaseIndent()
    {
        EditorGUI.indentLevel++;
    }
    private void DecreaseIndent()
    {
        EditorGUI.indentLevel--;
    }
    private void Space(float space)
    {
        EditorGUILayout.Space(space);
    }
    private void ApplyProperties()
    {
        serializedObject.ApplyModifiedProperties();
    }
    #endregion
}