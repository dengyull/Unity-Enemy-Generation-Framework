using UnityEngine;
using GEGFramework;
using System.Collections.Generic;

namespace GEGFramework {
    public class GEGManager : MonoBehaviour {
        static GEGPackedData packedData;
        static GEGDifficultyManager diffManager;
        static GEGUpdater updater;

        [SerializeField, Range(0, 10)] int defaultDiff; // prompt for default difficulty level for this scene
        [SerializeField] float spawnInterval = 3f;

        [SerializeField] int minHealth = 10, maxHealth = 100;
        [SerializeField] int minSpeed = 1, maxSpeed = 10;
        [SerializeField] float attackSpeed = 1f;
        [SerializeField] bool randomSpawn = false;
        [SerializeField] List<Transform> enemySpawnPoints;
        [SerializeField] List<GEGCharacterInst<double>> characters;

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
