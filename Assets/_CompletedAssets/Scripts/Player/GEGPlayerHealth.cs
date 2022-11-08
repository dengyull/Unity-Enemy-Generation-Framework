using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GEGFramework;

[CreateAssetMenu(fileName = "GEGPlayerHealth", menuName = "GEG Framework/GEG Player Health")]
public class GEGPlayerHealth : GEGProperty<int> {
    public GEGPlayerHealth(string propName, int value, int baseValue) : base(propName, value, baseValue) {
        propName = "health";
        value = 100;
        baseValue = 100;
    }
}
