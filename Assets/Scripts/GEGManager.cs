using UnityEngine;
using GEGFramework;
using System.Collections.Generic;

namespace GEGFramework {
    public class GEGManager : MonoBehaviour {
        [SerializeField, Range(0, 10)] int defaultDiff; // prompt for default difficulty level for this scene
        [SerializeField] float spawnInterval = 3f;
        [SerializeField] bool randomSpawn = false;
        [SerializeField] List<Transform> enemySpawnPoints;
        [SerializeField] List<GEGCharacter> characters;

        static GEGPackedData packedData;
        static GEGDifficultyManager diffManager;
        static GEGUpdater updater;        

        float spawnTimer;   // countdown timer for spawner
        float diffEvalTimer; // countdown timer for difficulty evaluation

        void Start() {
            updater = new GEGUpdater();
            packedData = new GEGPackedData(100f, 10f);

            diffEvalTimer = GEGPackedData.diffEvalInterval;
            diffManager = new GEGDifficultyManager(defaultDiff); // test values
            spawnTimer = spawnInterval; // init timer
        }

        void Update() {
            diffEvalTimer -= Time.deltaTime;
            spawnTimer -= Time.deltaTime; // timer countdown

            if (spawnTimer <= 0f) { // time to spawn a enemy

                spawnTimer = spawnInterval; // reset spawn timer
            }

            if (diffEvalTimer <= 0) { // time to change difficulty
                int newDiffLevel = diffManager.GetDifficulty(packedData, 3, 5, 3); // test values
                List<KeyValuePair<string, int>> newNumEnemy = updater.RunExample(newDiffLevel);
                string str = "";
                Debug.Log("-------------------------");
                for (int i = 0; i < newNumEnemy.Count; ++i) {
                    str += newNumEnemy[i].ToString() + ",";
                }
                Debug.Log(str);
                Debug.Log(newDiffLevel);
                Debug.Log("-------------------------");
                diffEvalTimer = GEGPackedData.diffEvalInterval;
            }
        }
    }
}
