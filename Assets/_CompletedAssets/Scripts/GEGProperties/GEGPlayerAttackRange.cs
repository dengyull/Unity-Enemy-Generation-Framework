using GEGFramework;
using UnityEngine;

[CreateAssetMenu(fileName = "GEGPlayerAttackRange", menuName = "GEG Framework/GEG Property/PlayerAttackRange")]
public class GEGPlayerAttackRange : GEGCharacterProperty<double> {
    public GEGPlayerAttackRange() : base("PlayerAttackRange", 100, 40, true) { }
}
