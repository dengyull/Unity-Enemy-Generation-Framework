using System;

namespace GEGFramework {
    public interface IGEGController {
        public static Action<float> valueChanged;
        GEGCharacter Character { get; set; }
    }
}