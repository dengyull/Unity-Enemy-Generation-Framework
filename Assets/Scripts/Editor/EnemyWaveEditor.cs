using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyWaveManager))]
public class EnemyWaveEditor : Editor {
    #region  SerializeProperties
    SerializedProperty spawnInterval;
    SerializedProperty enemyPrefabs;
    SerializedProperty enemySpawnPoints;
    SerializedProperty minHealth;
    SerializedProperty maxHealth;
    SerializedProperty minSpeed;
    SerializedProperty maxSpeed;
    #endregion

    // EnemyWaveManager manager;
    bool playerControlGroup = true;

    private void OnEnable() {
        // manager = target as EnemyWaveManager;
        spawnInterval = serializedObject.FindProperty("spawnInterval");
        enemyPrefabs = serializedObject.FindProperty("enemyPrefabs");
        enemySpawnPoints = serializedObject.FindProperty("enemySpawnPoints");
        minHealth = serializedObject.FindProperty("minHealth");
        maxHealth = serializedObject.FindProperty("maxHealth");
        minSpeed = serializedObject.FindProperty("minSpeed");
        maxSpeed = serializedObject.FindProperty("maxSpeed");
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();

        playerControlGroup = EditorGUILayout.BeginFoldoutHeaderGroup(playerControlGroup, "Player");
        if (playerControlGroup) { // fold content
            EditorGUILayout.HelpBox("Player controls will be displayed here.", MessageType.Warning);
        }
        EditorGUILayout.EndFoldoutHeaderGroup(); // end fold

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("Enemy Controls", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal(); // start horizontal layout
        EditorGUIUtility.labelWidth = 70;
        EditorGUILayout.PropertyField(minHealth);
        EditorGUILayout.PropertyField(maxHealth);
        EditorGUILayout.EndHorizontal(); // end horizontal layout

        EditorGUILayout.BeginHorizontal(); // start horizontal layout
        EditorGUIUtility.labelWidth = 70;
        EditorGUILayout.PropertyField(minSpeed);
        EditorGUILayout.PropertyField(maxSpeed);
        EditorGUILayout.EndHorizontal(); // end horizontal layout

        EditorGUIUtility.labelWidth = 100;
        EditorGUILayout.Slider(spawnInterval, 1f, 10f);
        EditorGUILayout.Space(5);
        EditorGUILayout.PropertyField(enemyPrefabs);
        EditorGUILayout.Space(5);
        EditorGUILayout.PropertyField(enemySpawnPoints);

        serializedObject.ApplyModifiedProperties();
    }
}
