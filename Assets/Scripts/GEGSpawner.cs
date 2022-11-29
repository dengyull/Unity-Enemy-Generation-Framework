using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GEGFramework {
    /// <summary>
    /// Realize output from GEGDifficultyManager
    /// </summary>
    class GEGSpawner : MonoBehaviour {
        public string enemyTag = "Enemy";

        int totalSpawn = 0; // total number of enemies to spawn in this wave
        bool startSpawning = false, spawning = false;
        Coroutine spawnCoroutine;

        void OnEnable() {
            GEGManager.OnNewWaveStart += (int _) => {
                if (!spawning) startSpawning = true; // start spawning on new wave
            };
        }

        void Update() {
            if (startSpawning && !spawning) {
                startSpawning = false;
                spawnCoroutine = StartCoroutine(SpawnEnemies());
            }
        }

        public bool haveAliveEnemies() {
            var enemies = GameObject.FindGameObjectsWithTag(enemyTag);
            if (enemies.Length == 0) return false;
            return true;
        }

        IEnumerator SpawnEnemies() {
            spawning = true;
            if (GEGPackedData.randomSpawn) {
                float waveInterval = GEGPackedData.maxWaveInterval;
                List<(GameObject Prefab, int Num)> temp = new List<(GameObject, int)>();

                foreach (GEGCharacter c in GEGPackedData.characters) {
                    if (c.nextWaveNum > 0 && c.type != GEGCharacterType.Player) {
                        temp.Add((c.prefab, c.nextWaveNum)); // populate temp array
                        totalSpawn += c.nextWaveNum;
                    }
                }
                while (totalSpawn > 0) { // while temp is not empty
                    int randType = Random.Range(0, temp.Count);
                    float randSpawnInterval = Random.Range(0, waveInterval / totalSpawn);
                    int randPoint = Random.Range(0, GEGPackedData.enemySpawnPoints.Count - 1); // spawn at random spawn point

                    var inst = Instantiate(temp[randType].Prefab, GEGPackedData.enemySpawnPoints[randPoint].position,
                        temp[randType].Prefab.transform.rotation);
                    inst.tag = enemyTag;
                    waveInterval -= randSpawnInterval;
                    yield return new WaitForSeconds(randSpawnInterval);

                    if (temp[randType].Num - 1 > 0) // if character[i] has more instances to be spawned
                        temp[randType] = (temp[randType].Prefab, temp[randType].Num - 1);
                    else // else remove this type of character from the list
                        temp.RemoveAt(randType);
                    totalSpawn--;
                }
            }
            if (spawnCoroutine != null) StopCoroutine(spawnCoroutine);
            spawning = false;
        }
    }
}