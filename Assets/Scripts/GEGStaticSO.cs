using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "GEG Framework/Static Inputs")]
public class GEGStaticSO : ScriptableObject {
    public List<GameObject> playerPrefabs; // store players' prefabs
    public List<GameObject> enemyPrefabs; // store enemies' prefabs
    public List<Transform> enemySpawnPoints; // initial enemies' spawn points
}
