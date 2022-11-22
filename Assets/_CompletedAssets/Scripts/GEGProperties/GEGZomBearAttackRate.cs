using GEGFramework;
using UnityEngine;

[CreateAssetMenu(fileName = "GEGZomBearAttackRate", menuName = "GEG Framework/GEG Property/ZomBear Attack Rate")]
public class GEGZomBearAttackRate : GEGCharacterProperty {
    public GEGZomBearAttackRate() : base("ZomBearAttackRate", 0.5f, 10, true) { }
}
