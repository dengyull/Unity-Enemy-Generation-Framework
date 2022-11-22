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

        // countdown timer for each wave
        float waveTimer;
        float diffEvalTimer; // countdown timer for difficulty evaluation

        public static event Action onWaveStarted;

        void Start() {
            new GEGPackedData(waveInterval, diffEvalInterval);
            diffManager = new GEGDifficultyManager(defaultDiff);
            diffEvalTimer = GEGPackedData.diffEvalInterval;
            waveTimer = GEGPackedData.waveInterval;
        }

        void Update() {
            // timers countdown:
            diffEvalTimer -= Time.deltaTime;
            waveTimer -= Time.deltaTime;
            

            if (waveInterval <= 0f) { // time to start next wave
                // ...
                onWaveStarted?.Invoke();
                waveTimer = GEGPackedData.waveInterval; // reset spawn timer
            }

            if (diffEvalTimer <= 0) { // time to change difficulty
                // ...
                diffEvalTimer = GEGPackedData.diffEvalInterval;
            }
        }

        public void UpdateData() {

        }
    }
}
