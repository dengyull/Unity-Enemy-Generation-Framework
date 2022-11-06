namespace GEGFramework {
    public interface GEGProperty<T> {
        string name { get; set; } // Property name (e.g. health)
        bool enabled { get; set; } // Enabled for difficulty evaluation?
        bool porportion { get; set; } // Whether the property value should be proportional to the typeDiff
        float diffWeight { get; set; } // How important is this property (prop) compared to other props in difficulty evaluation?
        T baseValue { get; set; } // Default value
        T value { get; set; } // Current value

        /// <summary>
        /// Update current value with new property values generated based on given difficulty level
        /// </summary>
        /// <param name="difficulty">Difficulty level (from 0 to 10)</param>
        /// <returns></returns>
        public void Update(int difficulty);

        /// <summary>
        /// Calculate value based on given difficulty level
        /// </summary>
        /// <param name="difficulty">Difficulty level (from 0 to 10)</param>
        /// <returns></returns>
        public T PropertyCalculateMethod(int difficulty);
    }
}