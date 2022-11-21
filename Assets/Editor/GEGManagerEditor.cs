using UnityEditor;
using GEGFramework;

[CustomEditor(typeof(GEGManager))]
public class GEGManagerEditor : Editor {

    #region SerializedProperties
    SerializedProperty defaultDiff;
    SerializedProperty spawnInterval;
    SerializedProperty randomSpawn;
    SerializedProperty enemySpawnPoints;
    SerializedProperty characters;
    #endregion

    private void OnEnable() {
        defaultDiff = serializedObject.FindProperty("defaultDiff");
        spawnInterval = serializedObject.FindProperty("spawnInterval");
        randomSpawn = serializedObject.FindProperty("randomSpawn");
        enemySpawnPoints = serializedObject.FindProperty("enemySpawnPoints");
        characters = serializedObject.FindProperty("characters");
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();
        EditorGUILayout.PropertyField(defaultDiff);
        EditorGUILayout.PropertyField(spawnInterval);
        EditorGUILayout.PropertyField(randomSpawn);
        EditorGUILayout.PropertyField(enemySpawnPoints);
        EditorGUILayout.PropertyField(characters);
        serializedObject.ApplyModifiedProperties();
    }
}
