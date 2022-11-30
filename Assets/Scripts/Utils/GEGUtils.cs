using System;

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
}