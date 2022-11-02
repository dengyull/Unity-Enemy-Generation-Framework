using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GEGPackedData {
    public static float currentPHealthPercent; // current player's health percentage
    public static float scoreEvalInterval; // time interval (in seconds) between each difficulty score evaluation
    public static float waveInterval; // time interval (in seconds) between each wave
    public static List<GEGEnemyTypeData> enemyTypeData; // a list packed data for each type of enemy
    public static Dictionary<string, (bool, float)> dictProperty; // stores key-values <propertyName : (enabled?, weight)>

    /// <summary>
    /// Default GEGPackedData constructor
    /// </summary>
    public GEGPackedData() {
        currentPHealthPercent = 1f;
        scoreEvalInterval = 10f;
        waveInterval = 100f;
        enemyTypeData = new List<GEGEnemyTypeData>();
        dictProperty = new Dictionary<string, (bool, float)>() {
            {"PlayerHealth", (true, 1f)}, {"PlayerAPM", (false, 0f)},{"PlayerAccuracy",(false, 0f)},
            {"PlayerScore", (true, 1f)}, {"EnemyHealth", (false, 1f)}
        };
    }
}

public class GEGCommunicator : MonoBehaviour {
    public GEGPackedData packedData;

    // public UnityEvent onPlayerKilled;
    // public UnityEvent onEnemyKilled;
    public UnityEvent onDifficultyChanged; // triggered when new difficulty level is computed

    //ScoreManager scoreManager;

    float scoreEvalTimer;
    int lastDifficultyScore, newDifficultyScore;

    void Start() {
        packedData = new GEGPackedData();
        scoreEvalTimer = GEGPackedData.scoreEvalInterval;
    }

    void Update() {
        scoreEvalTimer -= Time.deltaTime; // countdown for ComputeScore()
        if (scoreEvalTimer <= 0) {
            // newDifficultyScore = scoreManager.ComputeScore();
            onDifficultyChanged.Invoke();
        }
    }
}
