using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

//public class AssetHandler { // Handle events on related assets
//    [OnOpenAsset()]
//    public static bool OpenEditor(int instanceId, int line) {
//        EnemyWaveManager manager = EditorUtility.InstanceIDToObject(instanceId) as EnemyWaveManager;
//        if (manager != null) {
//            // Open EnemyWaveWindow when double clicked on an EnemyWaveManager instance
//            EnemyWaveWindow.ShowWindow();
//        }
//        return false;
//    }
//}

[CustomEditor(typeof(EnemyWaveManager))]
public class EnemyWaveEditor : Editor {

    #region  SerializedProperties
    // set of serialized properties in EnemyWaveManager
    SerializedProperty spawnInterval;
    SerializedProperty enemyPrefabs;
    SerializedProperty enemySpawnPoints;
    SerializedProperty minHealth;
    SerializedProperty maxHealth;
    SerializedProperty minSpeed;
    SerializedProperty maxSpeed;
    SerializedProperty attackSpeed;
    SerializedProperty randomSpawn;
    #endregion

    int _enemyChoiceIndex = 0; // store dropdown choice index
    bool playerControlGroup = true, typeControlGroup = true; // indicates the expansion of playerControlGroup toggle menu
    List<string> enemyPrefabsNames = new List<string>(); // store enemies' name (type names)

    private void OnEnable() {
        spawnInterval = serializedObject.FindProperty("spawnInterval");
        enemyPrefabs = serializedObject.FindProperty("enemyPrefabs");
        enemySpawnPoints = serializedObject.FindProperty("enemySpawnPoints");
        minHealth = serializedObject.FindProperty("minHealth");
        maxHealth = serializedObject.FindProperty("maxHealth");
        minSpeed = serializedObject.FindProperty("minSpeed");
        maxSpeed = serializedObject.FindProperty("maxSpeed");
        attackSpeed = serializedObject.FindProperty("attackSpeed");
        randomSpawn = serializedObject.FindProperty("randomSpawn");
        enemyPrefabsNames.Add("None");
    }

    public override void OnInspectorGUI() {
        serializedObject.Update(); // set serialized object stream based on what is in the actual class

        enemyPrefabsNames.Clear();
        enemyPrefabsNames.Add("None");
        for (int i = 0; i < enemyPrefabs.arraySize; i++) {
            // init enemies dropdown values (string list)
            if (enemyPrefabs.GetArrayElementAtIndex(i).objectReferenceValue != null) {
                // update name array
                enemyPrefabsNames.Add(enemyPrefabs.GetArrayElementAtIndex(i).objectReferenceValue.name);
            }
        }

        playerControlGroup = EditorGUILayout.BeginFoldoutHeaderGroup(playerControlGroup, "Player");
        if (playerControlGroup) { // fold content (toggle menu)
            EditorGUI.indentLevel++;
            EditorGUILayout.HelpBox("Player controls will be displayed here.", MessageType.Info);
            EditorGUI.indentLevel--;
        }
        EditorGUILayout.EndFoldoutHeaderGroup(); // end fold

        EditorGUILayout.Space(15);

        EditorGUI.BeginChangeCheck(); // begin check for property change
        EditorGUILayout.PropertyField(enemyPrefabs);
        if (EditorGUI.EndChangeCheck()) { // if changes happened
            Repaint(); // Repaint inspector if new prefabs added/removed
        }

        EditorGUILayout.LabelField("Enemy Type Controls", EditorStyles.boldLabel);
        if (enemyPrefabsNames.Count >= 1) {
            // Dropdown menu to select different type of enemies
            _enemyChoiceIndex = EditorGUILayout.Popup(_enemyChoiceIndex, enemyPrefabsNames.ToArray());
            if (_enemyChoiceIndex != 0) { // if "None" is not selected in dropdown menu
                typeControlGroup = EditorGUILayout.BeginFoldoutHeaderGroup(typeControlGroup, enemyPrefabsNames[_enemyChoiceIndex]);
                if (typeControlGroup) { // display specific enemy type controls
                    EditorGUI.indentLevel++;
                    EditorGUILayout.HelpBox(string.Format("Specific controls for {0} will be displayed here.",
                        enemyPrefabsNames[_enemyChoiceIndex]), MessageType.Info);

                    EditorGUILayout.BeginHorizontal(); // start horizontal layout
                    EditorGUIUtility.labelWidth = 90; // modify text label width to 70 pixels
                    EditorGUILayout.PropertyField(minHealth); // create a field for minHealth property
                    EditorGUILayout.PropertyField(maxHealth);
                    EditorGUILayout.EndHorizontal(); // end horizontal layout

                    EditorGUILayout.BeginHorizontal();
                    EditorGUIUtility.labelWidth = 90;
                    EditorGUILayout.PropertyField(minSpeed);
                    EditorGUILayout.PropertyField(maxSpeed);
                    EditorGUILayout.EndHorizontal();

                    EditorGUIUtility.labelWidth = 100;
                    EditorGUILayout.Slider(attackSpeed, 1f, 10f); // use slider to control attack speed
                    EditorGUI.indentLevel--;
                }
                EditorGUILayout.EndFoldoutHeaderGroup();
            }
        }

        EditorGUILayout.Space(5);
        EditorGUILayout.PropertyField(enemySpawnPoints); // Spawn points array
        EditorGUILayout.Space(5);
        EditorGUILayout.Slider(spawnInterval, 1f, 10f); // use slider to control spawnInterval
        EditorGUILayout.PropertyField(randomSpawn); // toggle randomSpawn

        //if (GUILayout.Button("Open Editor Window")) {
        //    EnemyWaveWindow.ShowWindow(); // Open editor window and pass an EnemyWaveManager
        //}

        serializedObject.ApplyModifiedProperties(); // apply changes been made to the serializedObject
    }

    void DrawGUILine(float i_height = 1.2f) {
        // draw a horizontal line in inspector; from https://forum.unity.com/threads/horizontal-line-in-editor-window.520812/
        Rect rect = EditorGUILayout.GetControlRect(false, i_height);
        rect.height = i_height;
        EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1));
    }
}
