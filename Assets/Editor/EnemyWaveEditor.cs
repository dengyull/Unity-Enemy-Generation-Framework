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

    #region  SerializeProperties
    //set of SerializeProperties in EnemyWaveManager
    SerializedProperty spawnInterval;
    SerializedProperty enemyPrefabs;
    SerializedProperty enemySpawnPoints;
    SerializedProperty minHealth;
    SerializedProperty maxHealth;
    SerializedProperty minSpeed;
    SerializedProperty maxSpeed;
    SerializedProperty attackSpeed;
    #endregion

    int _enemyChoiceIndex = 0;
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

        enemyPrefabsNames.Add("None");
    }

    public override void OnInspectorGUI() {
        serializedObject.Update(); // set serialized object stream based on what is in the actual class

        for (int i = 0; i < enemyPrefabs.arraySize; i++) {
            // init enemies dropdown values (string list)
            if (enemyPrefabs.GetArrayElementAtIndex(i).objectReferenceValue != null) {
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

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("Enemy Type Controls", EditorStyles.boldLabel);

        if (enemyPrefabsNames.Count >= 1) {
            // Dropdown menu to select different type of enemies
            _enemyChoiceIndex = EditorGUILayout.Popup(_enemyChoiceIndex, enemyPrefabsNames.ToArray());
            if (_enemyChoiceIndex != 0) {
                typeControlGroup = EditorGUILayout.BeginFoldoutHeaderGroup(typeControlGroup, enemyPrefabsNames[_enemyChoiceIndex]);
                if (typeControlGroup) {
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

                    EditorGUIUtility.labelWidth = 110;
                    EditorGUILayout.Slider(attackSpeed, 1f, 10f); // use slider to control attack speed
                    EditorGUILayout.Slider(spawnInterval, 1f, 10f); // use slider to control spawnInterval
                    EditorGUI.indentLevel--;
                }
                EditorGUILayout.EndFoldoutHeaderGroup();
            }
        }
        EditorGUILayout.Space(5); // add a space (5 pixels) in between

        EditorGUI.BeginChangeCheck(); // detect property change
        EditorGUILayout.PropertyField(enemyPrefabs);
        if (EditorGUI.EndChangeCheck()) {
            // if new prefabs added/removed
            Repaint();
        }
        EditorGUILayout.Space(5);
        EditorGUILayout.PropertyField(enemySpawnPoints);
        serializedObject.ApplyModifiedProperties(); // apply changes been made to a SerializedObject

        //if (GUILayout.Button("Open Editor Window")) {
        //    EnemyWaveWindow.ShowWindow(); // Open editor window and pass an EnemyWaveManager
        //}
    }
}
