using GEGFramework;
using UnityEngine;

[CreateAssetMenu(fileName = "GEGHellephantAttackRate", menuName = "GEG Framework/GEG Property/Hellephant Attack Rate")]
public class GEGHellephantAttackRate : GEGCharacterProperty {
    public GEGHellephantAttackRate() : base("HellephantAttackRate", 0.5f, 10, true) { }
}
