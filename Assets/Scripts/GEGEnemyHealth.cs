using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GEGFramework;

public class GEGEnemyHealth : GEGProperty<double>
{
    public string _name = "speed";
    public string name
    {
        get { return _name; }
        set { _name = value; }
    }
    public bool _enabled = true;
    public bool enabled
    {
        get { return _enabled; }
        set { _enabled = value; }
    }
    public bool _porportion = true;
    public bool porportion
    {
        get { return _porportion; }
        set { _porportion = value; }
    }
    public float _diffWeight = 1f;
    public float diffWeight
    {
        get { return _diffWeight; }
        set { _diffWeight = value; }
    }
    public double _baseValue = 0.1;
    public double baseValue
    {
        get { return _baseValue; }
        set { _baseValue = value; }
    }

    public double _value = 0.1;
    public double value
    {
        get { return _value; }
        set { _value = value; }
    }

    public void Update(int difficulty)
    {
        if (porportion)
        {
            value = baseValue * difficulty * diffWeight;
        }
        else
        {
            value = baseValue * diffWeight / difficulty;
        }
    }
}