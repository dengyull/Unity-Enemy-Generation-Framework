using System;
using UnityEngine;

namespace GEGFramework {
    [CreateAssetMenu(fileName = "Property", menuName = "GEG Framework/Character's Property")]
    public class GEGCharacterProperty : ScriptableObject {

        public string propertyName;
        public bool enabled;
        public float value; // template value to generate the property value in each instance
        public float importance;

        public override string ToString() => String.Format("{0}({1}):{2}", propertyName, value, importance);
    }
}