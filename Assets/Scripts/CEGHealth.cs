using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GEGFramework;

public class CEGHealth
{

    public float value;
    public float baseValue;

    public string name { get; set; }
    public bool enabled { get; set; }
    public bool porportion { get; set; }
    public float diffWeight { get; set; }
    public CEGHealth(string propName, float value, float baseValue, float diffWeight = 0f,bool enabled = false, bool porportion = true)
        //: base(propName, value, baseValue, 0f, false, true)
    {
        name = propName;
        this.value = value;
        this.baseValue = baseValue;
        this.diffWeight = diffWeight;
        this.enabled = enabled;
        this.porportion = porportion;
    }
}
