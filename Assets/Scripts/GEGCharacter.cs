using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace GEGFramework {
    [System.Serializable]
    public enum GEGCharacterType {
        Player,
        Enemy
    }

    [Serializable]
    public class GEGCharacterException : Exception {
        public GEGCharacterException(string message) : base(message) { }
    }

    [CreateAssetMenu(fileName = "GEGTypeContainer", menuName = "GEG Framework/GEG Type Container")]
    public class GEGCharacter : ScriptableObject {

        public GameObject prefab; // Prefab for this type of character
        public GEGCharacterType type; // type of character
        public string typeName; // Name for this type of character, usually equals prefab's name

        // Rename diffFactor to difficultyFactor in inspector:
        [FormerlySerializedAs("diffFactor")]
        [SerializeField] public float difficultyFactor; // Only used when GEGTypeContainer.type is GEGCharacterType.Enemy
        public float diffFactor {
            get { return difficultyFactor; }
            set { difficultyFactor = value; }
        }
        public List<GEGProperty<double>> defaultProperty; // A default property list

        /// <summary>
        /// Constructor for player type character
        /// </summary>
        /// <param name="playerName">Name of this container. This is usually the name of the player prefab</param>
        public GEGCharacter(string playerName) {
            typeName = playerName;
            diffFactor = -1f;
            type = GEGCharacterType.Player;
            defaultProperty = new List<GEGProperty<double>>();
        }

        /// <summary>
        /// Constructor for enemy type character
        /// </summary>
        /// <param name="enemyTypeName">Name of this container. This is usually the name of the enemy prefab</param>
        /// <param name="diffFactor">Range(0,10); Indicating how much influence this type of enemy has on difficulty evaluation</param>
        public GEGCharacter(string enemyTypeName, float diffFactor) {
            if (diffFactor < 0 || diffFactor > 10)
                throw new ArgumentOutOfRangeException("Param [diffFactor] must within the range of 0 to 10");
            typeName = enemyTypeName;
            type = GEGCharacterType.Enemy;
            this.diffFactor = diffFactor;
            defaultProperty = new List<GEGProperty<double>>();
        }

        /// <summary>
        /// Add a given property to the default property list
        /// </summary>
        /// <param name="prop">Given property</param>
        /// <returns>True if the property is added to the list, otherwise false</returns>
        public void AddProperty(GEGProperty<double> prop) {
            foreach (GEGProperty<double> p in defaultProperty) {
                if (prop.pName == p.pName)
                    throw new GEGCharacterException("There is already a property with the same name " +
                        "in the container. Please use another name.");
            }
            defaultProperty.Add(prop);
        }

        /// <summary>
        /// Search a property by property's name
        /// </summary>
        /// <param name="propName">Name of the desired property</param>
        /// <returns>Desired GEGProperty object; or NULL if not found</returns>
        public GEGProperty<double> FindProperty(string propName) {
            foreach (GEGProperty<double> prop in defaultProperty) {
                if (prop.pName == propName) return prop;
            }
            return null;
        }
    }
}