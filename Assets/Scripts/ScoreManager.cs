using UnityEngine;

public class ScoreManager
{
    public int curDif;
    private int previousPlayerHealth;
    private int maxHealth;
    private float passedTime;
    // Intialize the difficulty while creating the instance of this class
    public ScoreManager(int initialDif, int maximumHealth)
    {
        curDif = initialDif;
        previousPlayerHealth = maximumHealth;
        maxHealth = maximumHealth;
        passedTime = 0;
    }
    /* 
     * Return a difficulty level based on input
     * 
     *
     * curPlayerHealth: should be from 0 to max
     */
    public int getDifficulty(int curPlayerHealth, float updateDifInterval, float desiredPeakInterval, float desiredLowDifInterval, float desiredZeroDifInterval)
    {
        // Cases for difficulty = 0:
        // 1. No longer having good health
        if((curPlayerHealth / maxHealth) < 10 || (curPlayerHealth-previousPlayerHealth) / maxHealth > 0.5)
        {
            curDif = 0;
            passedTime = 0;
        }

        // 2. Peak for a long enough time
        else if (curDif >= 8 && passedTime > desiredPeakInterval)
        {
            curDif = 0;
            passedTime = 0;
        }

        // Cases for difficulty > 8
        // 1. The time is not long enough
        else if(curDif >= 8 && passedTime <= desiredPeakInterval)
        {
            
            int peakOrNot = Random.Range(0, 10);
            if (peakOrNot >= 7) { curDif = 0; passedTime = 0; }
            else if (peakOrNot < 7 && curDif != 10) { curDif += 1; passedTime += updateDifInterval; }

        }
        // 2. remain easy for a long time
        else if ((curDif < 8 || curPlayerHealth > 60) && passedTime >= desiredLowDifInterval)
        {
            curDif = 8;
        }


        // Cases for difficulty > 0 but < 8
        else if (curDif == 0 && passedTime >= desiredZeroDifInterval)
        {
            passedTime = 0;
            curDif = Random.Range(1, 5);
        }
        else if (curDif < 8 && passedTime < desiredLowDifInterval)
        {
            curDif++;
        }
        


        return curDif;
    }
}
