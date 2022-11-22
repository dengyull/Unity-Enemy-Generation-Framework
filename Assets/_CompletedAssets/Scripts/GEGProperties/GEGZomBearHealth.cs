using GEGFramework;
using UnityEngine;

[CreateAssetMenu(fileName = "GEGZomBearHealth", menuName = "GEG Framework/GEG Property/ZomBear Health")]
public class GEGZomBearHealth : GEGCharacterProperty {
    public GEGZomBearHealth() : base("ZomBearHealth", 100, 20, true) { }
}
