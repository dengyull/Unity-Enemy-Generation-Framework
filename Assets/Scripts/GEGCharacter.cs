using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace GEGFramework {
    [CreateAssetMenu(fileName = "GEGCharacter", menuName = "GEG Framework/Character")]
    public class GEGCharacter : ScriptableObject {
        [SerializeField]
        private string _name;
        public string Name {
            get => _name;
            set => _name = value;
        }

        public GameObject prefab;
        public GEGCharacterType type;
        public List<GEGCharacterProperty> propSO;
        public int nextWaveNum; // number of instances to spawn in next wave

        // Rename diffFactor to difficultyFactor in inspector:
        [FormerlySerializedAs("diffFactor")]
        [SerializeField] public float difficultyFactor; // Only used when GEGTypeContainer.type is GEGCharacterType.Enemy
        public float diffFactor {
            get { return difficultyFactor; }
            set { difficultyFactor = value; }
        }

        /// <summary>
        /// Constructor for player type character
        /// </summary>
        /// <param name="playerName">Name of this container. This is usually the name of the player prefab</param>
        public GEGCharacter(string playerName) {
            _name = playerName;
            type = GEGCharacterType.Player;
            diffFactor = -1f;
            nextWaveNum = -1;
        }

        /// <summary>
        /// Constructor for enemy type character
        /// </summary>
        /// <param name="enemyTypeName">Name of this container. This is usually the name of the enemy prefab</param>
        /// <param name="diffFactor">Range(0,10); Indicating how much influence this type of enemy has on difficulty evaluation</param>
        public GEGCharacter(string enemyName, float diffFactor) {
            if (diffFactor < 0 || diffFactor > 10)
                throw new ArgumentOutOfRangeException("Param [diffFactor] must within the range of 0 to 10");
            _name = enemyName;
            type = GEGCharacterType.Enemy;
            this.diffFactor = diffFactor;
            nextWaveNum = 0;
        }

        public GEGCharacterProperty this[string key] {
            get => propSO.Find(item => item.propName == key);
            set => propSO.Add(value);
        }
    }
}