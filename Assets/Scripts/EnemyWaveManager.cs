using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour {
    [SerializeField] float spawnInterval = 3f;
    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] List<Transform> enemySpawnPoints;
    [SerializeField] int minHealth, maxHealth;
    [SerializeField] int minSpeed, maxSpeed;

    float spawnTimer;

    // Start is called before the first frame update
    void Start() {
        spawnTimer = spawnInterval;
    }

    // Update is called once per frame
    void Update() {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0f) {
            GameObject enemy = Instantiate(enemyPrefabs[0], enemySpawnPoints[0].position, transform.rotation) as GameObject;
            spawnTimer = spawnInterval;
        }
    }
}
