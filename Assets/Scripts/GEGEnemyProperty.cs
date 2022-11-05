using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GEGEnemyProperty : MonoBehaviour
{
    string Name;
    int basevalue = 10;

    public virtual double PropertyCalculator(int difficultyLevel)
    {
        return basevalue;
    }

    public virtual string PropertyName()
    {
        return Name;
    }
}
