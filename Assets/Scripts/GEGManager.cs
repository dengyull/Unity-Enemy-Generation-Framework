using System;
using UnityEngine;
using System.Collections.Generic;

namespace GEGFramework {
    public class GEGManager : MonoBehaviour {

        public static event Action<int> OnNewWaveStart;

        [Tooltip("Expected time of completion (in seconds) for each wave"), SerializeField]
        float expectWaveTime;

        [Tooltip("Enable default spawning algorithm (randomly generated)"), SerializeField]
        bool defaultSpawning;

        [Tooltip("A list of GEGCharacter scriptable objects"), SerializeField]
        List<GEGCharacter> characters;

        [Tooltip("A list of Transform objects indicating possible spawn points"), SerializeField]
        List<Transform> enemySpawnPoints;

        GEGSpawner spawner;
        int waveCounter;
        float waveTimer; // countdown timer for each wave

        void Awake() {
            new GEGPackedData(); // initialize GEGPackedData
            UpdatePackedData();
            spawner = gameObject.GetComponent<GEGSpawner>();
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
            GEGPackedData.maxWaveInterval = expectWaveTime;
            GEGPackedData.randomSpawn = defaultSpawning;
            GEGPackedData.characters = characters;
            GEGPackedData.enemySpawnPoints = enemySpawnPoints;
        }
    }
}
