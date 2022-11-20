using System;
using System.Collections.Generic;
using UnityEngine;

namespace GEGFramework {

    [System.Serializable]
    public abstract class GEGProperty<T> : ScriptableObject {

        public List<T> values;
        public T baseValue;
        public float diffWeight;

        public string pName;
        public bool diffEnabled;
        public bool porportional;

        /// <summary>
        /// Default constructor for GEGProperty<T>
        /// </summary>
        /// <param name="propName">Property name (e.g. health)</param>
        /// <param name="firstValue"> First value to be stored in values</param>
        /// <param name="baseValue"> Default property value</param>
        public GEGProperty(string propName, List<T> values, T baseValue) {
            pName = propName;
            this.values = values;
            this.baseValue = baseValue;
            diffWeight = 0f;
            diffEnabled = false;
            porportional = true;
        }

        /// <summary>
        /// Default constructor for GEGProperty<T>
        /// </summary>
        /// <param name="propName">Property name (e.g. health)</param>
        /// <param name="values">Current property values for each instance under this type</param>
        /// <param name="baseValue"> Default property value</param>
        /// <param name="diffWeight">Range(0,10); How important is this property (prop) compared to other props 
        ///                          in difficulty evaluation?</param>
        /// <param name="enabled">Include this property in difficulty evaluation?</param>
        /// <param name="porportional">Whether the property value should be proportional to the difficulty level</param>
        public GEGProperty(string propName, List<T> values, T baseValue, float diffWeight, bool enabled, bool porportional) {
            pName = propName;
            this.values = values;
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
            switch (values, baseValue) {
                case (List<int> vals, int baseVal):
                    for (int i = 0; i < vals.Count; ++i)
                        vals[i] = (int)(baseVal * (diffWeight / 10) * (difficulty / 10));
                    break;
                case (List<float> vals, float baseVal):
                    for (int i = 0; i < vals.Count; ++i)
                        vals[i] = (float)baseVal * (diffWeight / 10f) * (difficulty / 10f);
                    break;
                case (List<double> vals, double baseVal):
                    for (int i = 0; i < vals.Count; ++i)
                        vals[i] = (double)baseVal * (diffWeight / 10f) * (difficulty / 10f);
                    break;
                default: throw new Exception("Invalid type");
            }
        }

        public override string ToString() => values.ToString();
    }
}