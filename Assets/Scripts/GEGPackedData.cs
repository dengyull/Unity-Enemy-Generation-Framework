using System.Collections.Generic;
using UnityEngine;

namespace GEGFramework {
    /// <summary>
    /// Packed data for GEG Framework.
    /// </summary>
    class GEGPackedData {
        public static bool randomSpawn;
        public static float maxWaveInterval; // Time interval (in seconds) between each wave
        public static List<Transform> enemySpawnPoints;
        public static List<GEGCharacter> characters; // Data of each type of character (in GEGCharacterInst type)

        /// <summary>
        /// Default GEGPackedData constructor
        /// </summary>
        public GEGPackedData(float waveInterval) {
            GEGPackedData.maxWaveInterval = waveInterval;
            characters = new List<GEGCharacter>();
        }
    }
}