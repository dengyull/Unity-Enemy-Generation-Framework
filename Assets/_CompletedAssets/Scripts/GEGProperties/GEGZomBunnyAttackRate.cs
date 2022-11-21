using GEGFramework;
using UnityEngine;

[CreateAssetMenu(fileName = "GEGZomBunnyAttackRate", menuName = "GEG Framework/GEG Property/ZomBunnyAttackRate")]
public class GEGZomBunnyAttackRate : GEGCharacterProperty<double> {
    public GEGZomBunnyAttackRate() : base("ZomBunnyAttackRate", 0.5, 10, true) { }
}
