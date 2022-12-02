using System;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GEGFramework {
    public class FrameworkManager : MonoBehaviour {

        public static event Action<int> OnNewWaveStart;

        [Tooltip("Expected time of completion (in seconds) for each wave"), SerializeField]
        float expectWaveTime;

        [Tooltip("Enable default spawning algorithm (randomly generated)"), SerializeField]
        bool defaultSpawning;

        [Tooltip("A list of GEGCharacter scriptable objects"), SerializeField]
        List<GEGCharacter> characters;

        [Tooltip("A list of Transform objects indicating possible spawn points"), SerializeField]
        List<Transform> enemySpawnPoints;

        Spawner spawner;
        int waveCounter;
        float waveTimer; // countdown timer for each wave

        void Awake() {
            new PackedData(); // initialize GEGPackedData
            UpdatePackedData();
            spawner = gameObject.GetComponent<Spawner>();
            waveCounter = 0;
            waveTimer = 0;
        }

        void Update() {
            // timers countdown:
            waveTimer -= Time.deltaTime;
            if (waveTimer <= 0) { // time to start next wave
                if (!spawner.HaveAliveEnemies()) {
                    waveCounter++;
                    OnNewWaveStart?.Invoke(waveCounter); // broadcast event
                    waveTimer = expectWaveTime; // reset spawn timer
                }
                // player takes longer than expected...
                // Debug.Log("Poor skill");
            }
        }

        public void UpdatePackedData() {
            PackedData.maxWaveInterval = expectWaveTime;
            PackedData.randomSpawn = defaultSpawning;
            PackedData.characters = characters;
            PackedData.enemySpawnPoints = enemySpawnPoints;
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(FrameworkManager))]
    public class FrameworkManagerEditor : Editor {

        #region SerializedProperties
        SerializedProperty expectWaveTime;
        SerializedProperty defaultSpawning;
        SerializedProperty characters;
        SerializedProperty enemySpawnPoints;
        #endregion

        FrameworkManager _target;

        private void OnEnable() {
            expectWaveTime = serializedObject.FindProperty("expectWaveTime");
            defaultSpawning = serializedObject.FindProperty("defaultSpawning");
            characters = serializedObject.FindProperty("characters");
            enemySpawnPoints = serializedObject.FindProperty("enemySpawnPoints");
            _target = target as FrameworkManager;
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
#endif
}