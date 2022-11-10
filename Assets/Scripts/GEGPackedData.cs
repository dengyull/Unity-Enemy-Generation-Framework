using System.Collections.Generic;
using UnityEngine;

namespace GEGFramework {
    /// <summary>
    /// Packed data for GEG Framework.
    /// </summary>
    public class GEGPackedData {
        public static float waveInterval; // Time interval (in seconds) between each wave
        public static float diffEvalInterval; // Time interval (in seconds) between each difficulty score evaluation
        public static List<GEGTypeContainer> playerData; // Packed data for each player
        public static List<GEGTypeContainer> enemyTypeData; // Packed data for each type of enemy

        // Stores key-values <propertyName : (enabled?, proportion?, base value, weight)>
        // public static Dictionary<string, (bool enabled, bool proportion, double baseVal, double weight)> dictProperty;

        /// <summary>
        /// Default GEGPackedData constructor
        /// </summary>
        public GEGPackedData() {
            waveInterval = 100f;
            diffEvalInterval = 5f;
            playerData = new List<GEGTypeContainer>();
            enemyTypeData = new List<GEGTypeContainer>();

            // Test cases
            GEGTypeContainer dumPlayer = new GEGTypeContainer("Player1");
            ScriptableObject propPHealth = ScriptableObject.CreateInstance<GEGProperty<double>>();
            // dumPlayer.AddProperty();
            playerData.Add(dumPlayer);

            enemyTypeData.Add(new GEGTypeContainer("Enemy1", 2f));
            enemyTypeData.Add(new GEGTypeContainer("Enemy2", 1.5f));
            enemyTypeData.Add(new GEGTypeContainer("Enemy3", 1f));
        }
    }
}