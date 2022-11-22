using GEGFramework;
using UnityEngine;

[CreateAssetMenu(fileName = "GEGZomBunnyAttackRate", menuName = "GEG Framework/GEG Property/ZomBunny Attack Rate")]
public class GEGZomBunnyAttackRate : GEGCharacterProperty {
    public GEGZomBunnyAttackRate() : base("ZomBunnyAttackRate", 0.5f, 10, true) { }
}
