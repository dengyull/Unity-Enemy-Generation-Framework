using System;
using UnityEngine;

namespace GEGFramework {
    public interface IGEGController {
        public static Action<float> valueChanged;

        [Tooltip("The GEGCharacter scriptable objects of this character")]
        GEGCharacter Character { get; set; }

        [Tooltip("The scaler of this property to the intensity")]
        float Scaler { get; set; }

        [Tooltip("This property has positive contribution (i.e. proportional to intensity) " +
            "to intensity if true, false otherwise")]
        bool Proportional { get; set; }
    }
}