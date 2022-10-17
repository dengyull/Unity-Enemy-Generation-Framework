using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour {
    [SerializeField] float spawnInterval = 3f;
    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] List<Transform> enemySpawnPoints;
    [SerializeField] int minHealth = 10, maxHealth = 100;
    [SerializeField] int minSpeed = 1, maxSpeed = 10;
    [SerializeField] float attackSpeed = 1f;

    float spawnTimer; // timer for spawn interval

    // Start is called before the first frame update
    void Start() {
        spawnTimer = spawnInterval; // init timer
    }

    // Update is called once per frame
    void Update() {
        spawnTimer -= Time.deltaTime; // timer countdown
        if (spawnTimer <= 0f) {
            GameObject enemy = Instantiate(enemyPrefabs[0], enemySpawnPoints[0].position,
                transform.rotation) as GameObject; // instantiate an enermy
            spawnTimer = spawnInterval; // reset timer
        }
    }
}
