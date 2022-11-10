using System.Collections.Generic;
using UnityEngine;

namespace GEGFramework {
    /// <summary>
    /// Update enemy numbers, attributes and locations based on difficulty level
    /// </summary>
    public class GEGLevelController {
        bool randomSpawn = true;
        List<Transform> enemySpawnPoints;

        //List<GEGTypeContainer> enemyTypeData;// = packedData.enemyTypeData;
        /// <summary>
        /// an example to run.
        /// </summary>
        /// <param name="difflevel">Difficulty level (from 0 to 10)</param>
        /// <returns></returns>
        public List<KeyValuePair<string, int>> RunExample(int diffLevel) {
            List <KeyValuePair<string, int>> res = EnemyNumberGenerator(diffLevel);
            EnemyPropertyGenerator(diffLevel);
            return res;
        }

        /// <summary>
        /// Generate enemy properties.
        /// </summary>
        /// <param name="difflevel">Difficulty level (from 0 to 10)</param>
        /// <param name="PropertyList">Dictionary contains all attributes with enable or not</param>
        /// <returns></returns>
        void EnemyPropertyGenerator(int difflevel) {
            for (int i = 0; i < GEGPackedData.enemyTypeData.Count; i++) {
                foreach (GEGProperty<double> kvp in GEGPackedData.enemyTypeData[i].defaultProperty) {
                    if (kvp.diffEnabled) {
                        kvp.UpdateProperty(difflevel);
                    }
                }
            }
        }

        /// <summary>
        /// Generate enemy number.
        /// </summary>
        /// <param name="difflevel">Difficulty level (from 0 to 10)</param>
        /// <returns></returns>
        List<KeyValuePair<string, int>> EnemyNumberGenerator(int difflevel)
        {
            List<KeyValuePair<string, int>> results = new List<KeyValuePair<string, int>>();
            List<KeyValuePair<string, float>> temp = new List<KeyValuePair<string, float>>();

            foreach (GEGTypeContainer enemy in GEGPackedData.enemyTypeData) {
                temp.Add(new KeyValuePair<string, float>(enemy.name, enemy.diffFactor));
            }
            temp.Sort((a, b) => a.Value.CompareTo(b.Value)); //sort enemy by diffFactor

            int upperBound = Mathf.CeilToInt(GEGPackedData.enemyTypeData.Count * difflevel / 10);
            for (int i = 0; i < upperBound; i++) {
				int ts = (int) (difflevel * temp[upperBound-i-1].Value * 10); //* base
                // int ts = (int)EnemyNumberCal(GEGPackedData.enemyTypeData[i].diffFactor, difflevel,
                // GEGPackedData.enemyTypeData[i].diffFactor, true);
                results.Add(new KeyValuePair<string, int>(GEGPackedData.enemyTypeData[i].name, ts));
            }
            return results;
        }
    }
}