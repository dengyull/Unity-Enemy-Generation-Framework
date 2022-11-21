using GEGFramework;
using UnityEngine;

[CreateAssetMenu(fileName = "GEGHellephantAttackDamage", menuName = "GEG Framework/GEG Property/HellephantAttackDamage")]
public class GEGHellephantAttackDamage : GEGCharacterProperty<double> {
    public GEGHellephantAttackDamage() : base("HellephantAttackDamage", 10, 30, true) { }
}
