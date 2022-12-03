using System;
using UnityEngine;

namespace GEGFramework {
    public interface IGEGController {
        GEGCharacter Character { get; set; }
        float Scaler { get; set; }
        bool Proportional { get; set; }
    }
}