using GEGFramework;
using UnityEngine;

[CreateAssetMenu(fileName = "GEGZomBunnyAttackDamage", menuName = "GEG Framework/GEG Property/ZomBunnyAttackDamage")]
public class GEGZomBunnyAttackDamage : GEGCharacterProperty<double> {
    public GEGZomBunnyAttackDamage() : base("ZomBunnyAttackDamage", 10, 30, true) { }
}
