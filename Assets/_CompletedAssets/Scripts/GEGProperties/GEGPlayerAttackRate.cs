using GEGFramework;
using UnityEngine;

[CreateAssetMenu(fileName = "GEGPlayerAttackRate", menuName = "GEG Framework/GEG Property/PlayerAttackRate")]
public class GEGPlayerAttackRate : GEGCharacterProperty<double> {
    public GEGPlayerAttackRate() : base("PlayerAttackRate", 0.15, 50, true) { }
}
