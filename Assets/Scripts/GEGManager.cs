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
        
        int waveCounter;
        float waveTimer; // countdown timer for each wave

        void Start() {
            new GEGPackedData(maxWaveInterval); // initialize GEGPackedData
            GEGPackedData.enemySpawnPoints = enemySpawnPoints;
            GEGPackedData.characters = characters;
            GEGPackedData.randomSpawn = randomSpawn;

            waveTimer = 0;
            waveCounter = 0;
        }

        void Update() {
            // timers countdown:
            waveTimer -= Time.deltaTime;

            if (waveTimer <= 0) { // time to start next wave
                waveCounter++;
                OnNewWaveStart?.Invoke(waveCounter); // broadcast event
                waveTimer = maxWaveInterval; // reset spawn timer
            }
        }

        public void UpdateData() {
            GEGPackedData.randomSpawn = randomSpawn;
            GEGPackedData.enemySpawnPoints = enemySpawnPoints;
            GEGPackedData.waveInterval = maxWaveInterval;
            GEGPackedData.characters = characters;
        }
    }
}
