using GEGFramework;
using UnityEngine;

[CreateAssetMenu(fileName = "GEGHellephantAttackRate", menuName = "GEG Framework/GEG Property/HellephantAttackRate")]
public class GEGHellephantAttackRate : GEGCharacterProperty<double> {
    public GEGHellephantAttackRate() : base("HellephantAttackRate", 0.5, 10, true) { }
}
