using GEGFramework;
using UnityEngine;

[CreateAssetMenu(fileName = "GEGPlayerHealth", menuName = "GEG Framework/GEG Property/Player Health")]
public class GEGPlayerHealth : GEGCharacterProperty {
    public GEGPlayerHealth() : base("PlayerHealth", 100, 20, true) { }
}
