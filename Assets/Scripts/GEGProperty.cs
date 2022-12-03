using System;
using UnityEngine;

namespace GEGFramework {
    [CreateAssetMenu(fileName = "Property", menuName = "GEG Framework/Character's Property")]
    public class GEGProperty : ScriptableObject {

        public string propertyName;
        public bool enabled;
        public float value;
        public float defaultValue;
        [Range(0, 100)]
        public float importance;

        public override string ToString() => String.Format("{0}({1}):{2}", propertyName, value, importance);
    }
}