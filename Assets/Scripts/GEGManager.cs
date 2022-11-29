using System;
using UnityEngine;
using System.Collections.Generic;

namespace GEGFramework {
    public class GEGManager : MonoBehaviour {

        public static event Action<int> OnNewWaveStart;

        [SerializeField] float maxWaveInterval;
        [SerializeField] bool randomSpawn;
        [SerializeField] List<GEGCharacter> characters;
        [SerializeField] List<Transform> enemySpawnPoints;

        GEGSpawner spawner;
        int waveCounter;
        float waveTimer; // countdown timer for each wave

        void Start() {
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
                if (!spawner.haveAliveEnemies()) {
                    waveCounter++;
                    OnNewWaveStart?.Invoke(waveCounter); // broadcast event
                    waveTimer = maxWaveInterval; // reset spawn timer
                }
                waveTimer = 3f;
                // player takes longer than expected...
                Debug.Log("Poor skill");
            }
        }

        public void UpdatePackedData() {
            GEGPackedData.maxWaveInterval = maxWaveInterval;
            GEGPackedData.randomSpawn = randomSpawn;
            GEGPackedData.characters = characters;
            GEGPackedData.enemySpawnPoints = enemySpawnPoints;
        }
    }
}
