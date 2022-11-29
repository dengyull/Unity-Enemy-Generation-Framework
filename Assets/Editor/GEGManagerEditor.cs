using UnityEditor;
using GEGFramework;

[CustomEditor(typeof(GEGManager))]
public class GEGManagerEditor : Editor {

    #region SerializedProperties
    SerializedProperty maxWaveInterval;
    SerializedProperty randomSpawn;
    SerializedProperty enemySpawnPoints;
    SerializedProperty characters;
    #endregion

    GEGManager _target;

    private void OnEnable() {
        maxWaveInterval = serializedObject.FindProperty("maxWaveInterval");
        randomSpawn = serializedObject.FindProperty("randomSpawn");
        characters = serializedObject.FindProperty("characters");
        enemySpawnPoints = serializedObject.FindProperty("enemySpawnPoints");
        _target = target as GEGManager;
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();
        EditorGUILayout.PropertyField(maxWaveInterval);
        EditorGUILayout.PropertyField(randomSpawn);
        EditorGUILayout.PropertyField(characters);
        EditorGUILayout.PropertyField(enemySpawnPoints);
        serializedObject.ApplyModifiedProperties();
        _target.UpdatePackedData();
    }
}
