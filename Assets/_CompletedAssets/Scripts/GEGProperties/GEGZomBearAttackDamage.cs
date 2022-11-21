using GEGFramework;
using UnityEngine;

[CreateAssetMenu(fileName = "GEGZomBearAttackDamage", menuName = "GEG Framework/GEG Property/ZomBear Attack Damage")]
public class GEGZomBearAttackDamage : GEGCharacterProperty {
    public GEGZomBearAttackDamage() : base("ZomBearAttackDamage", 10, 30, true) { }
}
