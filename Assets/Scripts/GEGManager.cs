using System;
using UnityEngine;
using System.Collections.Generic;

namespace GEGFramework {
    public class GEGManager : MonoBehaviour {
        [SerializeField, Range(0, 10)] int defaultDiff; // prompt for default difficulty level for this scene
        [SerializeField] float diffEvalInterval;
        [SerializeField] float waveInterval;
        [SerializeField] bool randomSpawn;
        [SerializeField] List<Transform> enemySpawnPoints;
        [SerializeField] List<GEGCharacter> characters;

        [SerializeField] static GEGPackedData packedData;
        static GEGDifficultyManager diffManager;

        int waveCounter;
        // countdown timer for each wave
        float waveTimer;
        float diffEvalTimer; // countdown timer for difficulty evaluation

        public static event Action<int> OnNewWaveStart;
        public static event Action<int> OnDiffChanged;

        void Start() {
            new GEGPackedData(waveInterval, diffEvalInterval);
            GEGPackedData.enemySpawnPoints = enemySpawnPoints;
            GEGPackedData.characters = characters;
            GEGPackedData.randomSpawn = randomSpawn;

            diffManager = new GEGDifficultyManager(defaultDiff);
            diffEvalTimer = GEGPackedData.diffEvalInterval;
            waveTimer = GEGPackedData.waveInterval;
            waveCounter = 0;
        }

        void Update() {
            // timers countdown:
            diffEvalTimer -= Time.deltaTime;
            waveTimer -= Time.deltaTime;

            if (waveTimer <= 0) { // time to start next wave
                waveCounter++;
                OnNewWaveStart?.Invoke(waveCounter); // broadcast event
                waveTimer = waveInterval; // reset spawn timer
            }

            if (diffEvalTimer <= 0) { // time to change difficulty
                OnDiffChanged?.Invoke(diffManager.formulaUpdate(3, 5, 3)); // test case
                diffEvalTimer = diffEvalInterval;
            }
        }

        public void UpdateData() {
            GEGPackedData.randomSpawn = randomSpawn;
            GEGPackedData.enemySpawnPoints = enemySpawnPoints;
            GEGPackedData.waveInterval = waveInterval;
            GEGPackedData.diffEvalInterval = diffEvalInterval;
            GEGPackedData.characters = characters;
        }
    }
}
