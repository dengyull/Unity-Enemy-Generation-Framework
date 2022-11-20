using GEGFramework;
using UnityEngine;

[CreateAssetMenu(fileName = "GEGHealth", menuName = "GEG Framework/GEG Property/Player Health")]
public class GEGPlayerHealth : GEGCharacterProperty<double> {
    public GEGPlayerHealth() : base("health", 100, 20, true) { }
}
