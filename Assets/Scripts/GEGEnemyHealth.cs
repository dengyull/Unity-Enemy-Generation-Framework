using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GEGEnemyHealth : GEGEnemyProperty
{
    string Name;
    int basevalue = 10;
    int difficulty = 5;


    public override double PropertyCalculator(int difficultyLevel)
    {
        return basevalue + difficulty * difficultyLevel;
    }

}
