using GEGFramework;
using UnityEngine;

[CreateAssetMenu(fileName = "GEGZomBunnyHealth", menuName = "GEG Framework/GEG Property/ZomBunnyHealth")]
public class GEGZomBunnyHealth : GEGCharacterProperty<double> {
    public GEGZomBunnyHealth() : base("ZomBunnyHealth", 100, 20, true) { }
}
