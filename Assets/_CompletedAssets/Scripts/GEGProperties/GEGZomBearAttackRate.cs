using GEGFramework;
using UnityEngine;

[CreateAssetMenu(fileName = "GEGZomBearAttackRate", menuName = "GEG Framework/GEG Property/ZomBearAttackRate")]
public class GEGZomBearAttackRate : GEGCharacterProperty<double> {
    public GEGZomBearAttackRate() : base("ZomBearAttackRate", 0.5, 10, true) { }
}
