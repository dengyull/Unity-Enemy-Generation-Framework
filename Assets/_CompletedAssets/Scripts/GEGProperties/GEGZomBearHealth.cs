using GEGFramework;
using UnityEngine;

[CreateAssetMenu(fileName = "GEGZomBearHealth", menuName = "GEG Framework/GEG Property/ZomBearHealth")]
public class GEGZomBearHealth : GEGCharacterProperty<double> {
    public GEGZomBearHealth() : base("ZomBearHealth", 100, 20, true) { }
}
