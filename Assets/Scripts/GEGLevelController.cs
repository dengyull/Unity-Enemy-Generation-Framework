using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GEGLevelController : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {

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

	[SerializeField] List<Transform> enemySpawnPoints;
    [SerializeField] GEGPackedData packedData;
    [SerializeField] bool randomSpawn = false;

    /*Dictionary<string, double> enemypropertyGenerator(int difflevel, List<GEGEnemyProperty> PropertyList)
    {
        Dictionary<string, double> re = new Dictionary<string, double>();
        for (int i = 0; i < PropertyList.Count; i++)
        {
            re.Add(PropertyList[i].PropertyName(), PropertyList[i].PropertyCalculator(difflevel));
        }
        return re;
    }*/
    Dictionary<string, double> enemypropertyGenerators(int difflevel, Dictionary PropertyList)
    {
        Dictionary<string, double> re = new Dictionary<string, double>();
        foreach (KeyValuePair<string, bool> kvp in PropertyList)
        {
            if (kvp.Value)
            {
                if (kvp.Key == "health")
                {
                    re.Add("health", todoHP(difflevel));
                } 
                else if (kvp.Key == "Speed")
                {
                    re.Add("Speed", todoSpeed(difflevel));
                }
                else if (kvp.Key == "AttackRate")
                {
                    re.Add("AttackRate", todoAttackRate(difflevel));
                }
            }
        }
        return re;
    }
    double PropertyCal(double difficultyEnem, int difficulty, double baseValue, bool v)
    {
        double re;
        if (v)
        {
            re = difficultyEnem * difficulty / baseValue;
        }
        else
        {
            re = difficultyEnem * baseValue * difficulty;
        }
        return re;
    }
    List<double> todoHP(int difflevel)
    {
        List<double> re = new List<double>();
        int enemyNumber = enemyPrefabs.Count;
        for (int i = 0; i < enemyNumber; i++)
        {
            re.Add(PropertyCal(difficultyHP[i], difflevel, baseHP[i],false));
        }
        return re;
    }


    List<double> todoSpeed(int difflevel)
    {
        List<double> re = new List<double>();
        int enemyNumber = enemyPrefabs.Count;
        for (int i = 0; i < enemyNumber; i++)
        {
            re.Add(PropertyCal(difficultySpeed[i], difflevel, baseSpeed[i], false));
        }
        return re;

    }


    List<double> todoAttackRate(int difflevel)
    {
        List<double> re = new List<double>();
        int enemyNumber = enemyPrefabs.Count;
        for (int i = 0; i < enemyNumber; i++)
        {
            re.Add(PropertyCal(difficultyAttackRate[i], difflevel, baseAttackRate[i], false));
        }
        return re;

    }

    void running(int difflevel)
    {
        enemypropertyGenerators(difflevel);
        List<int> enemys = enemyNumberGenerator(difflevel);
        enemypositionsGenerator(enemys);

    }

    double enemyNumberCal(float difficultyEnem, int difficulty, float baseValue, bool v)
    {
        double re;
        if (v)
        {
            re = difficultyEnem * difficulty / baseValue;
        }
        else
        {
            re = difficultyEnem * baseValue * difficulty;
        }
        return re;
    }
    int enemyPercentage(int difflevel)
    {
        return Mathf.RoundToInt(GEGPackedData.enemyTypeData.Count * difflevel / 10);
    }
    List<int> enemyNumberGenerator(int difflevel)
    {
        
        List<int> re = new List<int>();
        int t = enemyPercentage(difflevel);
        for (int i = 0; i < t; i++)
        {
            
            int ts = (int)enemyNumberCal(GEGPackedData.enemyTypeData[i].Item3, difflevel, GEGPackedData.enemyTypeData[i].Item2, true);
            re.Add(ts);
        }
        return re;
    }

    List<List<int>> enemypositionsGenerator(List<int> enemys)
    {
        List<List<int>> res = new List<List<int>>();
        for (int i = 0; i < enemys.Count; i++)
        {
            List<int> re = new List<int>();
            for (int j = 0; j < enemys[i]; j++)
            {
                re.Add(enemypositionGenerator());
            }
            res.Add(re);

        }
        return res;
    }

    // return Enemy Spawn Point from list randomly, or 0 point.could extend more strategies.
    int enemypositionGenerator()
    {

        if (randomSpawn)
        {
            return Random.Range(0, enemySpawnPoints.Count);
        }
        else
        {
            return 0;
        }
    }


}
