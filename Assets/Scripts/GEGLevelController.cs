using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GEGFramework
{
    /// <summary>
    /// Update enemy numbers, attributes and locations based on difficulty level
    /// </summary>
    public class GEGLevelController
    {
        List<Transform> enemySpawnPoints;
        bool randomSpawn = true;

        public GEGLevelController()
        {
        }


        //List<GEGTypeContainer> enemyTypeData;// = packedData.enemyTypeData;
        /// <summary>
        /// an example to run.
        /// </summary>
        /// <param name="difflevel">Difficulty level (from 0 to 10)</param>
        /// <returns></returns>
        public void RunExample(GEGPackedData data, int diffLevel)
        {
            List<int> enemys = enemyNumberGenerator(GEGPackedData.enemyTypeData, diffLevel);
            enemypropertyGenerators(diffLevel, GEGPackedData.enemyTypeData);
            List<List<int>> position = enemypositionsGenerator(enemys);
            for (int i = 0; i < enemys.Count; i++)
            {
                for (int j = 0; j < enemys[i]; j++)
                {
                    Instantiate(GEGPackedData.enemyTypeData[i].prefab, enemySpawnPoints[position[i][j]].position);
                }

            }
        }

        /// <summary>
        /// Generate enemy properties.
        /// </summary>
        /// <param name="difflevel">Difficulty level (from 0 to 10)</param>
        /// <param name="PropertyList">Dictionary contains all attributes with enable or not</param>
        /// <returns></returns>
        void enemypropertyGenerators(int difflevel, List<GEGTypeContainer> enemyTypeData)
        {
            for (int i = 0; i < enemyTypeData.Count; i++)
            {
                //enemypropertyGenerator(difflevel, enemyTypeData[i].defaultProperty);
                foreach (GEGProperty<double> kvp in enemyTypeData[i].defaultProperty)
                {
                    if (kvp.enabled)
                    {
                        kvp.Update(difflevel);
                    }
                }
            }
        }

        /// <summary>
        /// Helper functions, computational purposes
        /// </summary>
        /// <returns></returns>
        private double enemyNumberCal(float difficultyEnem, int difficulty, float baseValue, bool v)
        {
            if (v)
            {
                return difficultyEnem * difficulty / baseValue;
            }
            else
            {
                return difficultyEnem * baseValue * difficulty;
            }
        }

        /// <summary>
        /// Generate enemy number.
        /// </summary>
        /// <param name="difflevel">Difficulty level (from 0 to 10)</param>
        /// <returns></returns>
        List<int> enemyNumberGenerator(List<GEGTypeContainer> enemies, int difflevel)
        {
            enemies.Sort((a, b) => a.diffFactor.CompareTo(b.diffFactor));//sort enemy by diffFactor
            List<int> re = new List<int>();
            int t = Mathf.RoundToInt(enemies.Count * difflevel / 10);
            for (int i = 0; i < t; i++)
            {
                int ts = (int)enemyNumberCal(enemies[i].diffFactor, difflevel, enemies[i].diffFactor, true);
                re.Add(ts);
            }
            return re;
        }
        /// <summary>
        /// Generate each enemies position.
        /// </summary>
        /// <param name="difflevel">Difficulty level (from 0 to 10)</param>
        /// <returns></returns>
        List<List<int>> enemypositionsGenerator(List<int> enemys)
        {
            List<List<int>> res = new List<List<int>>();
            for (int i = 0; i < enemys.Count; i++)
            {
                List<int> re = new List<int>();
                for (int j = 0; j < enemys[i]; j++)
                {

                    if (randomSpawn)
                    {
                        re.Add(Random.Range(0, enemySpawnPoints.Count));
                    }
                    else
                    {
                        if (enemySpawnPoints.Count >= i)
                        {
                            re.Add(i);
                        }
                        else
                        {
                            re.Add(enemySpawnPoints.Count);
                        }

                    }
                }
                res.Add(re);
            }
            return res;
        }

    }

}