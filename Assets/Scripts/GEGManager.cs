using UnityEngine;
using GEGFramework;
using UnityEngine.Events;

public class GEGManager : MonoBehaviour {
    // public UnityEvent onPlayerKilled;
    // public UnityEvent onEnemyKilled;
    public UnityEvent onDifficultyChanged; // triggered when new difficulty level is computed

    static GEGPackedData packedData;
    static GEGScoreManager scoreManager;
    static GEGLevelController controller;

    [SerializeField, Range(0, 10)] int defaultDiff; // prompt for default difficulty level for this scene

    float diffEvalTimer; // countdown timer for difficulty evaluation

    void Start() {
        packedData = new GEGPackedData();
        diffEvalTimer = GEGPackedData.diffEvalInterval;
        scoreManager = new GEGScoreManager(defaultDiff); // test values
    }

    void Update() {
        diffEvalTimer -= Time.deltaTime;
        if (diffEvalTimer <= 0) {
            //newDiffLevel = scoreManager.GetDifficulty(50, 3f, 3f, 3f, 3f); // test values
            //controller.UpdateProperties(newDiffLevel, packedData);
            onDifficultyChanged.Invoke();
        }
    }
}
