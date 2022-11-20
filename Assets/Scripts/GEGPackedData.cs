using System.Collections.Generic;
using UnityEngine;

namespace GEGFramework {
    /// <summary>
    /// Packed data for GEG Framework.
    /// </summary>
    class GEGPackedData {
        public static float waveInterval; // Time interval (in seconds) between each wave
        public static float diffEvalInterval; // Time interval (in seconds) between each difficulty score evaluation
        public static GEGCharacter playerData; // Packed data for players
        public static List<IGEGCharacter> characters; // Packed data for each type of enemy

        /// <summary>
        /// Default GEGPackedData constructor
        /// </summary>
        public GEGPackedData(float waveInterval, float diffEvalInterval) {
            GEGPackedData.waveInterval = waveInterval;
            GEGPackedData.diffEvalInterval = diffEvalInterval;
            characters = new List<IGEGCharacter>();
        }

        public void Test() { // Add test data
            GEGCharacter dumPlayer = new GEGCharacter("Player1");
            ScriptableObject propPHealth = ScriptableObject.CreateInstance<GEGCharacterProperty<double>>();

            characters.Add(new GEGCharacter("Enemy1", 2f));
            characters.Add(new GEGCharacter("Enemy2", 1.5f));
            characters.Add(new GEGCharacter("Enemy3", 1f));
        }
    }
}