using UnityEngine;

/// <summary>
/// Responsible for computing a new difficulty level (score)
/// </summary>
public class GEGScoreManager {
    // Q: if one function works, why declaring these class variables?
    public int currDiff; // Current difficulty level
    private int prevPlayerHealth; // Player's health in previous score computation; ISSUE: health should be float
    private int maxPlayerHealth; // Maximum player health; ISSUE: health should be float
    private float passedTime; // Q: Passed seconds since last score computation? If not, shouldn't named passedTime

    /// <summary>
    /// GEGScoreManager constructor
    /// </summary>
    /// <param name="initialDiff">Initial difficulty level</param>
    /// <param name="maxPlayerHealth">Maximum player health</param>
    public GEGScoreManager(int initialDiff, int maxPlayerHealth) {
        currDiff = initialDiff;
        prevPlayerHealth = maxPlayerHealth;
        this.maxPlayerHealth = maxPlayerHealth;
        passedTime = 0f;
    }

    /// <summary>
    /// Returns a difficulty level (score) based on the inputs
    /// </summary>
    /// <param name="currPlayerHealth">Current player health within range [0, maxPlayerHealth]</param>
    /// <param name="updateDiffInterval">Q: Not sure what is this?</param>
    /// <param name="peakInterval">Desired peak interval</param>
    /// <param name="lowDiffInterval">Desired low difficulty interval</param>
    /// <param name="zeroDiffInterval">Desired zero difficulty interval</param>
    /// <returns>New difficulty Level</returns>
    public int GetDifficulty(int currPlayerHealth, float updateDiffInterval, float peakInterval,
        float lowDiffInterval, float zeroDiffInterval) {
        // Q: Cases for letting new difficulty = 0?
        if ((currPlayerHealth / maxPlayerHealth) < 10
            || (currPlayerHealth - prevPlayerHealth) / maxPlayerHealth > 0.5) { // 1. No longer having good health
            // ISSUE: Conditions are incorrect: 1. curh/maxh always <= 1; 2. why gain >50% health is a bad health?
            currDiff = 0; // Set easy mode
            passedTime = 0; 
        } else if (currDiff >= 8 && passedTime > peakInterval) { // 2. Peak for a long enough time
            currDiff = 0;
            passedTime = 0;
        }
        // Q: Cases for letting new difficulty = 0 < difficulty < 8?
        else if (currDiff == 0 && passedTime >= zeroDiffInterval) {
            passedTime = 0;
            currDiff = Random.Range(1, 5);
        } else if (currDiff < 8 && passedTime < lowDiffInterval) {
            currDiff++;
        }
        // Q: Cases for letting new difficulty > 8?
        else if (currDiff >= 8 && passedTime <= peakInterval) { // 1. The time is not long enough
            int peakOrNot = Random.Range(0, 10);
            if (peakOrNot >= 7) { // Q: 30% new difficulty = 0?
                currDiff = 0;
                passedTime = 0;
            } else if (peakOrNot < 7 && currDiff != 10) { // Q: 70% increase difficulty?
                currDiff += 1;
                passedTime += updateDiffInterval; // Q: Why only here should passedTime be increased?
            }
        } else if ((currDiff < 8 || currPlayerHealth > 60)
            && passedTime >= lowDiffInterval) { // 2. Remain easy for a long time
            currDiff = 8;
        }
        return currDiff;
    }
}
