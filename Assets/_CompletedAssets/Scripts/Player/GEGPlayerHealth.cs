using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GEGFramework;

[CreateAssetMenu(fileName = "GEGPlayerHealth", menuName = "GEG Framework/GEG Player Health")]
public class GEGPlayerHealth : GEGProperty<double> {
    public GEGPlayerHealth(string propName = "health", double value = 100, double baseValue = 100)
        : base(propName, value, baseValue) { }
}
