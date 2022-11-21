using GEGFramework;
using UnityEngine;

[CreateAssetMenu(fileName = "GEGPlayerDamage", menuName = "GEG Framework/GEG Property/PlayerDamage")]
public class GEGPlayerDamage : GEGCharacterProperty<double> {
    public GEGPlayerDamage() : base("PlayerDamage", 10, 40, true) { }
}
