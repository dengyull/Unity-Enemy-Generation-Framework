using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "Enemy Wave Framework/Enemy Controls")]
public class EnemyScriptableObject : ScriptableObject {
    public float spawnInterval = 3f;
    public List<GameObject> enemyPrefabs;
    public List<Transform> enemySpawnPoints;
    public int minHealth, maxHealth;
    public int minSpeed, maxSpeed;
}
