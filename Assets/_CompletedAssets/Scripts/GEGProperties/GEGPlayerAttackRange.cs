using GEGFramework;
using UnityEngine;

[CreateAssetMenu(fileName = "GEGPlayerAttackRange", menuName = "GEG Framework/GEG Property/Player Attack Range")]
public class GEGPlayerAttackRange : GEGCharacterProperty {
    public GEGPlayerAttackRange() : base("PlayerAttackRange", 100, 40, true) { }
}
