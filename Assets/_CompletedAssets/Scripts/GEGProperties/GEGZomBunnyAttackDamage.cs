using GEGFramework;
using UnityEngine;

[CreateAssetMenu(fileName = "GEGZomBunnyAttackDamage", menuName = "GEG Framework/GEG Property/ZomBunny Attack Damage")]
public class GEGZomBunnyAttackDamage : GEGCharacterProperty {
    public GEGZomBunnyAttackDamage() : base("ZomBunnyAttackDamage", 10, 30, true) { }
}
