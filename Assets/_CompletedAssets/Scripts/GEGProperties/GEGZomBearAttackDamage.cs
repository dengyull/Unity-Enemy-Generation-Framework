using GEGFramework;
using UnityEngine;

[CreateAssetMenu(fileName = "GEGZomBearAttackDamage", menuName = "GEG Framework/GEG Property/ZomBearAttackDamage")]
public class GEGZomBearAttackDamage : GEGCharacterProperty<double> {
    public GEGZomBearAttackDamage() : base("ZomBearAttackDamage", 10, 30, true) { }
}
