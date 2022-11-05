using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GEGEnemySpeed : GEGEnemyProperty
{
    string Name;
    int basevalue = 10;
    int difficulty = 1;


    public override double PropertyCalculator(int difficultyLevel)
    {
        return basevalue = difficulty / difficultyLevel;
    }

}
