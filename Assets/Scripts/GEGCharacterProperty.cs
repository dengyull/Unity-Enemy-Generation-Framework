using System;
using UnityEngine;

namespace GEGFramework {
    public abstract class GEGCharacterProperty<T> : ScriptableObject {

        public T value;
        public string propName;
        public float diffWeight;
        public bool diffEnabled;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propName"></param>
        /// <param name="value"></param>
        /// <param name="enabled"></param>
        public GEGCharacterProperty(string propName, T value, float weight, bool enabled) {
            this.propName = propName;
            this.value = value;
            diffWeight = weight;
            diffEnabled = enabled;
        }

        public override string ToString() => String.Format("{0}:\n\tcurrent value: {1}\n\tdiffWeight:{2}\n\tenabled:{3}.",
            propName, value, diffWeight, diffEnabled);
    }
}