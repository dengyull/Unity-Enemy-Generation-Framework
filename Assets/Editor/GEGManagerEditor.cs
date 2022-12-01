using UnityEditor;
using GEGFramework;

[CustomEditor(typeof(GEGManager))]
public class GEGManagerEditor : Editor {

    #region SerializedProperties
    SerializedProperty expectWaveTime;
    SerializedProperty defaultSpawning;
    SerializedProperty characters;
    SerializedProperty enemySpawnPoints;
    #endregion

    GEGManager _target;

    private void OnEnable() {
        expectWaveTime = serializedObject.FindProperty("expectWaveTime");
        defaultSpawning = serializedObject.FindProperty("defaultSpawning");
        characters = serializedObject.FindProperty("characters");
        enemySpawnPoints = serializedObject.FindProperty("enemySpawnPoints");
        _target = target as GEGManager;
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();
        EditorGUILayout.PropertyField(expectWaveTime);
        EditorGUILayout.PropertyField(defaultSpawning);
        EditorGUILayout.PropertyField(characters);
        EditorGUILayout.PropertyField(enemySpawnPoints);
        serializedObject.ApplyModifiedProperties();
        _target.UpdatePackedData();
    }
}
