using System;
using Random = UnityEngine.Random;

namespace GEGFramework {
    /// <summary>
    /// Responsible for computing a new difficulty level (score)
    /// </summary>
    public class GEGScoreManager {

        int prevDiff; // Previous difficulty level
        int numRounds; // Number of rounds for calculating difficulty level

        // Counting rounds for staying at different difficulty level:
        (int zeroRounds, int lowRounds, int peakRounds) counters = (0, 0, 0);

        /// <summary>
        /// GEGScoreManager constructor
        /// </summary>
        /// <param name="defaultDiff">Initial difficulty level</param>
        /// <param name="maxPlayerHealth">Maximum player health</param>
        public GEGScoreManager(int defaultDiff) {
            if (defaultDiff < 0 || defaultDiff > 10)
                throw new ArgumentOutOfRangeException("Default difficulty level must be between 0 and 10.");
            numRounds = 0;
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
        public int GetDifficulty(int zeroDuration, int lowDuration, int peakDuration) {

            int newDiff = prevDiff; // New difficulty level to return
            numRounds++; // Means 10 seconds passed if GEGPackedData.diffEvalInterval = 10f

            switch (prevDiff) { // update round counters
                case 0:
                    counters.zeroRounds++; // rounds (stay in zero difficulty level) + 1
                    break;
                case int diff when diff < 7:
                    counters.lowRounds++; // rounds (stay in 1-7 difficulty level) + 1 //Low difficulty should be 1-6
                    break;
                case int diff when diff <= 10:
                    counters.peakRounds++; // rounds (stay in 8-10 difficulty level) + 1
                    break;
            }

            // Cases for difficulty equals to 0
            if (prevDiff == 0) {
                // Enough time to relax, so we set difficulty to very low level including 1, 2, 3. 
                if (counters.zeroRounds > zeroDuration) {
                    counters.zeroRounds = 0;
                    newDiff = Random.Range(1, 4);
                }
                // Else not enough time to relax, so we continue 0 difficulty so do nothing
            } else if (prevDiff > 0 && prevDiff < 7) { // Cases for low difficulty from 1 to 6
                // Enough time for low level difficulty, so we switch to high level difficulty.
                if (counters.lowRounds > lowDuration) {
                    counters.lowRounds = 0;
                    newDiff = 7;
                } else { // Not enough time in low level
                    //Not reached the highest level for low difficulty yet, so we make the difficulty a little bit higher
                    if (prevDiff < 6) {
                        newDiff += 1;
                    }
                }
            } else if (prevDiff >= 7) { // Cases for high difficulty 7,8,9
                if (counters.peakRounds > peakDuration) {
                    counters.peakRounds = 0;
                    newDiff = 0;
                } else { // The peak time is not long enough
                    int continuePeakOrNot = Random.Range(0, 10);

                    if (continuePeakOrNot >= 7) { // 30% new difficulty = 0
                        counters.peakRounds = 0;
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