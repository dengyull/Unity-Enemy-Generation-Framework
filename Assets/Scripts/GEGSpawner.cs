using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GEGFramework {
    /// <summary>
    /// Realize output from GEGDifficultyManager
    /// </summary>
    class GEGSpawner : MonoBehaviour {
        bool startSpawning = false;

        private void OnEnable() {
            GEGManager.OnWaveStarted += () => startSpawning = true; // subscribe to difficulty changed event
        }

        private void Update() {
            if (startSpawning)
                StartCoroutine(SpawnEnemies());
        }

        IEnumerator SpawnEnemies() {
            if (startSpawning) {
                startSpawning = false;
                //Debug.Log(GEGPackedData.randomSpawn);
                if (GEGPackedData.randomSpawn) {
                    List<(GameObject Prefab, int Num)> temp = new List<(GameObject, int)>();

                    foreach (GEGCharacter c in GEGPackedData.characters) {
                        if (c.nextWaveNum > 0)
                            temp.Add((c.prefab, c.nextWaveNum));
                    }

                    while (temp.Count > 0) {
                        int randSpawnInterval = Random.Range(0, (int)GEGPackedData.waveInterval / 10);
                        for (int i = 0; i < temp.Count; ++i) { // for each type of character, do:
                            int randCount = Random.Range(0, temp[i].Num); // spawne random number of enemies of this type
                            int randPoint = Random.Range(0, GEGPackedData.enemySpawnPoints.Count);
                            for (int j = 0; j < randCount; ++j) { // spawn [randCount] enemies
                                Instantiate(temp[i].Prefab, GEGPackedData.enemySpawnPoints[randPoint].position,
                                    temp[i].Prefab.transform.rotation);
                            }
                            if (temp[i].Num > 0) // if no more instances need to be spawned
                                temp[i] = (temp[i].Prefab, temp[i].Num - randCount);
                            else
                                temp.RemoveAt(i);
                        }
                        yield return new WaitForSeconds(randSpawnInterval);
                    }
                }
            }
        }

        //private void OnDisable() {
        //    GEGManager.OnWaveStarted -= SpawnEnemies;
        //}
    }
}