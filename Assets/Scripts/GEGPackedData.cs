using System.Collections.Generic;
using UnityEngine;

namespace GEGFramework {
    /// <summary>
    /// Packed data for GEG Framework.
    /// </summary>
    public class GEGPackedData {
        public static float waveInterval; // Time interval (in seconds) between each wave
        public static float diffEvalInterval; // Time interval (in seconds) between each difficulty score evaluation
        public static GEGCharacter playerData; // Packed data for players
        public static List<GEGCharacter> enemyTypeData; // Packed data for each type of enemy

        // Stores key-values <propertyName : (enabled?, proportion?, base value, weight)>
        // public static Dictionary<string, (bool enabled, bool proportion, double baseVal, double weight)> dictProperty;

        /// <summary>
        /// Default GEGPackedData constructor
        /// </summary>
        public GEGPackedData() {
            waveInterval = 100f;
            diffEvalInterval = 5f;
            playerData = new GEGCharacter("players");
            enemyTypeData = new List<GEGCharacter>();

            // Test cases
            GEGCharacter dumPlayer = new GEGCharacter("Player1");
            ScriptableObject propPHealth = ScriptableObject.CreateInstance<GEGProperty<double>>();

            enemyTypeData.Add(new GEGCharacter("Enemy1", 2f));
            enemyTypeData.Add(new GEGCharacter("Enemy2", 1.5f));
            enemyTypeData.Add(new GEGCharacter("Enemy3", 1f));
        }
    }
}