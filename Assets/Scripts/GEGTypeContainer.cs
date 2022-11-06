using System;
using System.Collections.Generic;
using UnityEngine;

namespace GEGFramework {
    public enum GEGCharacterType {
        Player,
        Enemy
    }

    public class GEGTypeContainer {
        GEGCharacterType type;
        public GameObject prefab { get; set; } // Prefab for this type of character
        public float diffFactor; // Only used when GEGTypeContainer.type is GEGCharacterType.Enemy
        public string name { get; set; } // Name for this type of character, usually equals prefab's name
        public Dictionary<string, GEGProperty<double>> dictBasicProperty;

        /// <summary>
        /// Constructor for player type character
        /// </summary>
        /// <param name="playerName">Name of this container. This is usually the name of the player prefab</param>
        public GEGTypeContainer(string playerName) {
            name = playerName;
            type = GEGCharacterType.Player;
            dictBasicProperty = new Dictionary<string, GEGProperty<double>>();
        }

        /// <summary>
        /// Constructor for enemy type character
        /// </summary>
        /// <param name="enemyTypeName">Name of this container. This is usually the name of the enemy prefab</param>
        /// <param name="diffFactor">Range(0,10); Indicating how much influence this type of enemy has on difficulty evaluation</param>
        public GEGTypeContainer(string enemyTypeName, float diffFactor) {
            if (diffFactor < 0 || diffFactor > 10)
                throw new ArgumentOutOfRangeException("[diffFactor] must within the range of 0 to 10");
            name = enemyTypeName;
            type = GEGCharacterType.Enemy;
            this.diffFactor = diffFactor;
            dictBasicProperty = new Dictionary<string, GEGProperty<double>>();
        }

        public GEGProperty<double> this[string key] {

            get { return dictBasicProperty[key]; } // returns value if exists

            set { dictBasicProperty[key] = value; } // updates if exists, adds if doesn't exist

        }
    }
}