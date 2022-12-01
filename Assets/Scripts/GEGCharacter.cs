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

        // Rename diffFactor to difficultyFactor in inspector:
        [FormerlySerializedAs("diffFactor")]
        [SerializeField] public float difficultyFactor; // Only used when GEGTypeContainer.type is GEGCharacterType.Enemy
        public float diffFactor {
            get { return difficultyFactor; }
            set { difficultyFactor = value; }
        }

        public GEGCharacterProperty this[string key] {
            get => properties.Find(item => item.propertyName == key);
            set => properties.Add(value);
        }
    }
}