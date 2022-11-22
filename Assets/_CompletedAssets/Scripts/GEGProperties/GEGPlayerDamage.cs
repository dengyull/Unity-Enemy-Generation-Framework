using GEGFramework;
using UnityEngine;

[CreateAssetMenu(fileName = "GEGPlayerDamage", menuName = "GEG Framework/GEG Property/Player Damage")]
public class GEGPlayerDamage : GEGCharacterProperty {
    public GEGPlayerDamage() : base("PlayerDamage", 10, 40, true) { }
}
