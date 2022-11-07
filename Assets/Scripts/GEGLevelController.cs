using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GEGFramework;
/// <summary>
/// Update enemy numbers, attributes and locations based on difficulty level
/// </summary>
public class GEGLevelController : MonoBehaviour {

    List<Transform> enemySpawnPoints;
    GEGPackedData packedDatas;
    [SerializeField] bool randomSpawn = false;
    bool SpanSignal;
    float diffGenTimer;
    [SerializeField] int DiffLevel;
    void Start()
    {
        diffGenTimer = GEGPackedData.diffEvalInterval;
    }

    void Update()
    {
        diffGenTimer -= Time.deltaTime;
        if (diffGenTimer <= 0 || SpanSignal)
        {
            RunExample(DiffLevel, GEGPackedData.enemyTypeData);
            diffGenTimer -= GEGPackedData.diffEvalInterval;
        }
    }


    /// <summary>
    /// update diffcult level.
    /// </summary>
    /// <param name="difflevel">Difficulty level (from 0 to 10)</param>
    /// <returns></returns>
    public void DiffChange(int NewDiffLevel)
    {
        DiffLevel = NewDiffLevel;
    }

    //List<GEGTypeContainer> enemyTypeData;// = packedData.enemyTypeData;
    /// <summary>
    /// an example to run.
    /// </summary>
    /// <param name="difflevel">Difficulty level (from 0 to 10)</param>
    /// <param name="PropertyList">Dictionary contains all attributes with enable or not</param>
    /// <returns></returns>
    public void RunExample(int difflevel, List<GEGTypeContainer> PropertyList)
    {
        List<int> enemys = enemyNumberGenerator(difflevel);
        enemypropertyGenerators(difflevel, GEGPackedData.enemyTypeData);
        List<List<int>> position = enemypositionsGenerator(enemys);
        for (int i = 0; i < enemys.Count; i++)
        {
            for (int j = 0; j < enemys[i]; j++)
            {
                Instantiate(PropertyList[i].prefab, enemySpawnPoints[position[i][j]].position, transform.rotation);
                //Debug.Log("enemy type " + i + ", health: " + enemyproperty[i]["health"] + ", speed: " + enemyproperty[i]["Speed"] + ". location SpawnPoints" + position[i][j]);
            }

        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="propertyList">Specifies which property the developer wants to modify</param>
    /// <param name="gameDiff">The desired game difficulty for now</param>
    /// <param name="typeDiff">A ranking of enemy types according to their difficulty</param>
    /// <param name="dictBaseVals">Storing the base value for each property</param>
    /// <param name="dictPropotion">Stores a bool indicating whether the property value should 
    ///                             be proportional to the typeDiff</param>
    /// <returns>Property values</returns>
    /// 
    /// Q: Why is the dictBaseVals for each property not for each enemy type?
    /// 
    /// public Dictionary<string, float[]> UpdateProperties(int gameDiff, GEGPackedData packedData) { // new constructor
    /// 
    public Dictionary<string, float[]> UpdateProperties(int gameDiff, float[] typeDiff,
        Dictionary<string, bool> dictProperty, Dictionary<string, int> dictBaseVals, Dictionary<string, bool> dictPropotion) {

        //float[] typeDiff = new float[packedData.enemyTypeData.Length];
        //for (int i = 0; i < packedData.enemyTypeData.Length; ++i) {
        //    // Construct what you need below
        //}

        Dictionary<string, float[]> propertyValues = new Dictionary<string, float[]>();

        int numEnemyType = typeDiff.Length; // number of all enemy types
        gameDiff /= 10; // convert difficulty level to percentage

        // From endIndex to the end of array, the difficulty of those types should be 0 which means ignoring them
        // Should use Mathf.Ceil to round up
        int endIndex = (int)Mathf.Ceil(numEnemyType * gameDiff / 10); // Q: why not use gameDiffPercent?
        for (int i = endIndex; i < numEnemyType; i++) {
            typeDiff[i] = 0;
        }

        // Loop properties that need to be modified
        foreach (var item in dictProperty) {
            if (item.Value == true) {
                // The array containing values of the property for different types of enemies
                float[] values = new float[numEnemyType];

                if (dictPropotion[item.Key] == true) { // proportional [1,2,3,4,5] -> [1,2,3,4,5]
                    for (int i = 0; i < numEnemyType; i++) {
                        values[i] = gameDiff * typeDiff[i] * dictBaseVals[item.Key];
                    }
                } else { // inversely proportional [1,2,3,4,5] -> [5,4,3,2,1]
                    for (int i = endIndex - 1, j = 0; i >= 0; i--, j++) {
                        values[i] = gameDiff * typeDiff[j] * dictBaseVals[item.Key];
                    }
                }
                propertyValues[item.Key] = values;
            }
        }
        return propertyValues;
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
            return difficultyEnem* difficulty / baseValue;
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
    List<int> enemyNumberGenerator(int difflevel)
    {
        GEGPackedData.enemyTypeData.Sort((a, b) => a.diffFactor.CompareTo(b.diffFactor));//sort enemy by diffFactor
        List<int> re = new List<int>();
        int t = Mathf.RoundToInt(GEGPackedData.enemyTypeData.Count * difflevel / 10);
        for (int i = 0; i < t; i++)
        {
            
            int ts = (int)enemyNumberCal(GEGPackedData.enemyTypeData[i].diffFactor, difflevel, GEGPackedData.enemyTypeData[i].diffFactor, true);
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
                    } else
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
