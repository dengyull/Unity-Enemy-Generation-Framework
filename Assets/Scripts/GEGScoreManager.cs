using System;

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
        public int GetDifficulty(GEGPackedData data, int zeroDuration, int lowDuration, int peakDuration) {
            numRounds++; // Means 10 seconds passed if GEGPackedData.diffEvalInterval = 10f
            switch(prevDiff) { // update round counters
                case 0:
                    counters.zeroRounds++; // rounds (stay in zero difficulty level) + 1
                    break;
                case int diff when diff < 8:
                    counters.lowRounds++; // rounds (stay in 1-7 difficulty level) + 1
                    break;
                case int diff when diff <= 10:
                    counters.peakRounds++; // rounds (stay in 8-10 difficulty level) + 1
                    break;
            }
            int newDiff = prevDiff; // New difficulty level to return
            for (int i = 0; i < GEGPackedData.playerData.Count; ++i) { // For each player do...
                double currHealth = GEGPackedData.playerData[i].Find("health").value; // Get health property of this player
                // TODO: Assessing player conditions...
                // TODO: Perhaps also obtain the targeted player (weaker player) here?
            }

            // TODO: base on players' status evaluate diffLevels:
            if (counters.peakRounds > peakDuration) {
                // Peak difficulty level duration reached => Already enough time spent at peak diffLevel
                counters.peakRounds = 0; // Reset peak rounds counter
                // TODO: compute a lower diffLevel => update newDiff
            }
            // TODO: Conditions for other level duration reached...
            // else if ...
            return newDiff;
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
        //public int GetDifficulty(int currPlayerHealth, float updateDiffInterval, float peakInterval,
        //    float lowDiffInterval, float zeroDiffInterval) {
        //    // Q: Cases for letting new difficulty = 0?
        //    if ((currPlayerHealth / maxPlayerHealth) < 10
        //        || (currPlayerHealth - prevPlayerHealth) / maxPlayerHealth > 0.5) { // 1. No longer having good health
        //                                                                            // ISSUE: Conditions are incorrect: 1. curh/maxh always <= 1; 2. why gain >50% health is a bad health?
        //        currDiff = 0; // Set easy mode
        //        passedTime = 0;
        //    } else if (currDiff >= 8 && passedTime > peakInterval) { // 2. Peak for a long enough time
        //        currDiff = 0;
        //        passedTime = 0;
        //    }
        //    // Q: Cases for letting new difficulty = 0 < difficulty < 8?
        //    else if (currDiff == 0 && passedTime >= zeroDiffInterval) {
        //        passedTime = 0;
        //        currDiff = Random.Range(1, 5);
        //    } else if (currDiff < 8 && passedTime < lowDiffInterval) {
        //        currDiff++;
        //    }
        //    // Q: Cases for letting new difficulty > 8?
        //    else if (currDiff >= 8 && passedTime <= peakInterval) { // 1. The time is not long enough
        //        int peakOrNot = Random.Range(0, 10);
        //        if (peakOrNot >= 7) { // Q: 30% new difficulty = 0?
        //            currDiff = 0;
        //            passedTime = 0;
        //        } else if (peakOrNot < 7 && currDiff != 10) { // Q: 70% increase difficulty?
        //            currDiff += 1;
        //            passedTime += updateDiffInterval; // Q: Why only here should passedTime be increased?
        //        }
        //    } else if ((currDiff < 8 || currPlayerHealth > 60)
        //        && passedTime >= lowDiffInterval) { // 2. Remain easy for a long time
        //        currDiff = 8;
        //    }
        //    return currDiff;
        //}
    }

}
