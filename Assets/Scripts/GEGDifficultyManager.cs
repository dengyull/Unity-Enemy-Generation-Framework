using System;
using Random = UnityEngine.Random;
using System.Collections.Generic;
using UnityEngine;

namespace GEGFramework {
    /// <summary>
    /// Responsible for computing a new difficulty level (score)
    /// </summary>
    class GEGDifficultyManager {

        int prevDiff; // Previous difficulty level
        int currentRounds;  // Counting rounds for staying at the current difficulty level:

        /// <summary>
        /// GEGScoreManager constructor
        /// </summary>
        /// <param name="defaultDiff">Initial difficulty level</param>
        public GEGDifficultyManager(int defaultDiff) {
            if (defaultDiff < 0 || defaultDiff > 10)
                throw new ArgumentOutOfRangeException("Default difficulty level must be between 0 and 10.");
            currentRounds = 0;
            prevDiff = defaultDiff;
            EnemyNumberUpdate(defaultDiff);
        }

        /// <summary>
        /// Returns a difficulty level (score) based on the inputs
        /// </summary>
        /// <param name="packedData"></param>
        /// <param name="zeroDuration">Desired zero difficulty duration, counted in diffEval rounds</param>
        /// <param name="lowDuration">Desired low difficulty duration</param>
        /// <param name="peakDuration">Desired peak duration</param>
        /// <returns></returns>
        public int GetDifficulty(int zeroDuration, int lowDuration, int peakDuration) {

            int newDiff = prevDiff; // New difficulty level to return
            currentRounds++; // Means 10 seconds passed if GEGPackedData.diffEvalInterval = 10f

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

        /// <summary>
        /// previous solution.
        /// </summary>
        /// <param name="zeroDuration">Desired zero difficulty duration, counted in diffEval rounds</param>
        /// <param name="lowDuration">Desired low difficulty duration</param>
        /// <param name="peakDuration">Desired peak duration</param>
        /// <returns>Enemy Number.</returns>
        public int formulaUpdate(int zeroDuration, int lowDuration, int peakDuration) {
            int Difficulty = GetDifficulty(zeroDuration, lowDuration, peakDuration);
            EnemyPropertyGenerator(Difficulty);
            EnemyNumberUpdate(Difficulty);
            return Difficulty;
        }

        /// <summary>
        /// Generate enemy properties.
        /// </summary>
        /// <param name="difflevel">Difficulty level (from 0 to 10)</param>
        /// <param name="PropertyList">Dictionary contains all attributes with enable or not</param>
        /// <returns></returns>
        public void EnemyPropertyGenerator(int difflevel) {
            List<KeyValuePair<string, int>> results = new List<KeyValuePair<string, int>>();

            foreach (GEGCharacter character in GEGPackedData.characters) {
                if (character.type == GEGCharacterType.Enemy) {
                    foreach (GEGCharacterProperty prop in character.propSO) {
                        if (prop.diffEnabled) {
                            //Debug.Log("property" + prop.propName + "before : " + prop.value);
                            prop.value = prop.defaultValue + prop.defaultValue * difflevel / 10;
                            //Debug.Log("property" + prop.propName + "after: " + prop.value);
                            //results.Add();
                            //kvp.UpdateProperty(difflevel);
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Generate enemy number.
        /// </summary>
        /// <param name="difflevel">Difficulty level (from 0 to 10)</param>
        /// <returns></returns>
        public void EnemyNumberGenerator(int difflevel) {
            List<KeyValuePair<string, float>> temp = new List<KeyValuePair<string, float>>();
            float totalDiffcult = 0;
            foreach (GEGCharacter character in GEGPackedData.characters) {
                if (character.type == GEGCharacterType.Enemy) {
                    temp.Add(new KeyValuePair<string, float>(character.Name, character.diffFactor));
                    totalDiffcult += character.diffFactor;
                }
            }
            int upperBound = Mathf.CeilToInt(temp.Count * difflevel / 10);
            /*foreach (GEGTypeContainer enemy in GEGPackedData.enemyTypeData) {
                temp.Add(new KeyValuePair<string, float>(enemy.name, enemy.diffFactor));
            }*/
            temp.Sort((a, b) => a.Value.CompareTo(b.Value)); //sort enemy by diffFactor

            for (int i = 0; i < upperBound; i++) {
                GEGPackedData.characters[upperBound - i - 1].nextWaveNum = (int)(difflevel * temp[upperBound - i - 1].Value * 10);
            }

        }
        /// <summary>
        /// Generate enemy number.
        /// </summary>
        /// <param name="difflevel">Difficulty level (from 0 to 10)</param>
        /// <returns></returns>
        void EnemyNumberUpdate(int difflevel)
        {
            List<KeyValuePair<int, float>> temp = new List<KeyValuePair<int, float>>();
            float totalDiffcult = 0;

            for (int i = 0; i < GEGPackedData.characters.Count; i++)
            {
                if (GEGPackedData.characters[i].type == GEGCharacterType.Enemy)
                {
                    temp.Add(new KeyValuePair<int, float>(i, GEGPackedData.characters[i].diffFactor));
                }

            }
            int upperBound = Mathf.CeilToInt((temp.Count * difflevel) / (float)10);
            for (int i = 0; i < upperBound; i++)
            {
                totalDiffcult += temp[i].Value;
            }
            //Debug.Log("upperBound " + upperBound + ",temp.Count " + temp.Count+ ",diffcult level " + difflevel+",upperBound cal" + (temp.Count * difflevel)/10.0);
            temp.Sort((a, b) => a.Value.CompareTo(b.Value)); //sort enemy by diffFactor
            int tn = 0;
            //float totaldiffseed = difflevel * difflevel * Mathf.Log(difflevel, 2) + 1;* Mathf.Log(difflevel + 1, 2)
            float totaldiffseed = difflevel  * totalDiffcult + 1;
            int totalnumber = 0;
            for (int i = 0; i < upperBound - 1; i++)
            {
                float a = Random.Range(temp[upperBound - i - 1].Value, totaldiffseed * 2 / 3);
                tn = Mathf.Min(Mathf.FloorToInt(a / temp[upperBound - i - 1].Value), 5);
                //Debug.Log("number before: " + GEGPackedData.characters[upperBound - i - 1].nextWaveNum);
                GEGPackedData.characters[temp[upperBound - i - 1].Key].nextWaveNum = tn;
                //Debug.Log(temp[upperBound - i - 1].Key + " " + tn + " " + GEGPackedData.characters[temp[upperBound - i - 1].Key].nextWaveNum);
                totalnumber += tn;
                //Debug.Log("enemy "+(upperBound - i - 1) +" number : " + GEGPackedData.characters[upperBound - i - 1].nextWaveNum);
                totaldiffseed = totaldiffseed - tn * temp[upperBound - i - 1].Value;
            }
            for (int i = Mathf.Max(upperBound,0); i < temp.Count; i++)
            {
                GEGPackedData.characters[temp[i].Key].nextWaveNum = 0;
            }
            //Debug.Log("totaldiffseed " + totaldiffseed + "temp[0].Value " + temp[0].Value);
            tn = Mathf.Min(Mathf.RoundToInt(totaldiffseed / temp[0].Value), 5);
            totalnumber += tn;
            //tn = Mathf.RoundToInt(totaldiffseed / temp[0].Value);
            //Debug.Log("number before: " + GEGPackedData.characters[0].nextWaveNum);
            GEGPackedData.characters[temp[0].Key].nextWaveNum = tn;
            //Debug.Log("enemy 0 number : " + GEGPackedData.characters[0].nextWaveNum);
            //Debug.Log("number after: " + GEGPackedData.characters[0].nextWaveNum);
            Debug.Log("diffcult level " + difflevel + ", enemy number total: " + totalnumber);
            //Debug.Log("diffcult level " + difflevel + ", enemy number total: " + totalnumber + "totaldiffseed: " + totaldiffseed + " seed " + GEGPackedData.characters[1].nextWaveNum + " seed " + GEGPackedData.characters[2].nextWaveNum + " seed " + GEGPackedData.characters[3].nextWaveNum);
        }
    }
}