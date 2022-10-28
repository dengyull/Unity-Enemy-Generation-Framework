using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "Enemy Wave Framework/Static level Inputs")]
public class GEGStaticSO : ScriptableObject {
    public List<GameObject> playerPrefabs;
    public List<GameObject> enemyPrefabs;
    public List<Transform> enemySpawnPoints;
}
