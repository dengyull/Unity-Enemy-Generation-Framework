using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace GEGFramework {
    [CreateAssetMenu(fileName = "GEGCharacter", menuName = "GEG Framework/Character")]
    public class GEGCharacter : ScriptableObject {
        public GameObject prefab;
        public GEGCharacterType type;
        public List<GEGCharacterProperty> properties;
        public int numNextWave; // number of instances to spawn in next wave

        public GEGCharacterProperty this[string key] {
            get => properties.Find(item => item.propertyName == key);
            set => properties.Add(value);
        }
    }
}