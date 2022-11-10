using System;
using System.Collections.Generic;
using UnityEngine;

namespace GEGFramework {

    [System.Serializable]
    public abstract class GEGProperty<T> : ScriptableObject {

        public List<T> value;
        public T baseValue;
        public float diffWeight;

        public string pName;
        public bool diffEnabled;
        public bool porportional;
        
        /// <summary>
        /// Default constructor for GEGProperty<T>
        /// </summary>
        /// <param name="propName">Property name (e.g. health)</param>
        /// <param name="value"> First value to be stored</param>
        /// <param name="baseValue"> Default property value</param>
        public GEGProperty(string propName, T firstValue, T baseValue) {
            pName = propName;
            this.value = new List<T>();
            this.value.Add(firstValue);
            this.baseValue = baseValue;
            this.diffWeight = 0f;
            diffEnabled = false;
            this.porportional = true;
        }

        /// <summary>
        /// Default constructor for GEGProperty<T>
        /// </summary>
        /// <param name="propName">Property name (e.g. health)</param>
        /// <param name="value"> Current stored value</param>
        /// <param name="baseValue"> Default property value</param>
        /// <param name="diffWeight">Range(0,10); How important is this property (prop) compared to other props 
        ///                          in difficulty evaluation?</param>
        /// <param name="enabled">Include this property in difficulty evaluation?</param>
        /// <param name="porportional">Whether the property value should be proportional to the difficulty level</param>
        public GEGProperty(string propName, List<T> value, T baseValue, float diffWeight, bool enabled, bool porportional) {
            pName = propName;
            this.value = value;
            this.baseValue = baseValue;
            this.diffWeight = diffWeight;
            diffEnabled = enabled;
            this.porportional = porportional;
        }

        /// <summary>
        /// Update current value with new property values generated based on given difficulty level
        /// </summary>
        /// <param name="difficulty">Difficulty level (from 0 to 10)</param>
        /// <returns></returns>
        public virtual void UpdateProperty(int difficulty) { // Allow for override
            switch (value, baseValue) {
                case (int val, int baseVal):
                    val = (int)(baseVal * diffWeight / 10 * difficulty / 10); // Q: Will this.value be updated?
                    break;
                case (float val, float baseVal):
                    val = (float)baseVal * diffWeight / 10f * difficulty / 10f;
                    break;
                case (double val, double baseVal):
                    val = (double)baseVal * diffWeight / 10f * difficulty / 10f;
                    break;
                default: throw new Exception("Invalid type");
            }
        }

        public override string ToString() => value.ToString();
    }
}