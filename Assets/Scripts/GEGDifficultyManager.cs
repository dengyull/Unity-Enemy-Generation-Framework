using GEGFramework;
using System;
using Random = UnityEngine.Random;

namespace GEGDifficultyManager {
    /// <summary>
    /// Responsible for computing a new difficulty level (score)
    /// </summary>
    public class GEGDifficultyManager {

        int prevDiff; // Previous difficulty level
        int numRounds; // Number of total rounds since the game began
        int currentRounds;  // Counting rounds for staying at the current difficulty level:

        /// <summary>
        /// GEGScoreManager constructor
        /// </summary>
        /// <param name="defaultDiff">Initial difficulty level</param>
        public GEGDifficultyManager(int defaultDiff) {
            if (defaultDiff < 0 || defaultDiff > 10)
                throw new ArgumentOutOfRangeException("Default difficulty level must be between 0 and 10.");
            numRounds = 0;
            currentRounds = 0;
            prevDiff = defaultDiff;
        }

        /// <summary>
        /// Returns a difficulty level (score) based on the inputs
        /// </summary>
        /// <param name="data"></param>
        /// <param name="zeroDuration">Desired zero difficulty duration, counted in diffEval rounds</param>
        /// <param name="lowDuration">Desired low difficulty duration</param>
        /// <param name="peakDuration">Desired peak duration</param>
        /// <returns></returns>
        public int GetDifficulty(GEGPackedData packedData, int zeroDuration, int lowDuration, int peakDuration) {
            
            int newDiff = prevDiff; // New difficulty level to return
            numRounds++; // Means 10 seconds passed if GEGPackedData.diffEvalInterval = 10f
            currentRounds++; 


            // Cases for difficulty equals to 0 where no enemy should show up
            if (prevDiff == 0) {
                // Enough time to relax, so we set difficulty to very low level including 1, 2, 3. 
                if (currentRounds > zeroDuration) {
                    currentRounds = 0;
                    newDiff = Random.Range(1, 4);
                }
                // Else not enough time to relax, so we continue 0 difficulty so do nothing
            }


            // Cases for low difficulty: from 1 to 6
            else if (prevDiff > 0 && prevDiff < 7) { 
                // Enough time for low level difficulty, so we switch to high level difficulty.
                if (currentRounds > lowDuration) {
                    currentRounds = 0;
                    newDiff = 7;
                } else { // Not enough time in low level
                    //Not reached the highest level for low difficulty yet, so we make the difficulty a little bit higher
                    if (prevDiff < 6) {
                        newDiff += 1;
                    }
                }
            }

            // Cases for high difficulty: 7,8,9
            else if (prevDiff >= 7) { 
                // enough peak time, go to relax mode
                if (currentRounds > peakDuration) {
                    currentRounds = 0;
                    newDiff = 0;
                } else { // The peak time is not long enough
                    int continuePeakOrNot = Random.Range(0, 10);

                    if (continuePeakOrNot >= 7) { // 30% new difficulty = 0
                        currentRounds = 0;
                        newDiff = 0;
                    } else { // 70% increase difficulty
                        if (prevDiff < 9) {
                            newDiff += 1;
                        }
                    }
                }
            }

            prevDiff = newDiff;
            return newDiff;
        }
    }
}