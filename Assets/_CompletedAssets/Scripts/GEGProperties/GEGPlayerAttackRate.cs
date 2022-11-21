using GEGFramework;
using UnityEngine;

[CreateAssetMenu(fileName = "GEGPlayerAttackRate", menuName = "GEG Framework/GEG Property/Player Attack Rate")]
public class GEGPlayerAttackRate : GEGCharacterProperty {
    public GEGPlayerAttackRate() : base("PlayerAttackRate", 0.15, 50, true) { }
}
