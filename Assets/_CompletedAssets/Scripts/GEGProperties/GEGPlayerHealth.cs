using GEGFramework;
using UnityEngine;

[CreateAssetMenu(fileName = "GEGPlayerHealth", menuName = "GEG Framework/GEG Property/PlayerHealth")]
public class GEGPlayerHealth : GEGCharacterProperty<double> {
    public GEGPlayerHealth() : base("PlayerHealth", 100, 20, true) { }
}
