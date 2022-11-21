using GEGFramework;
using UnityEngine;

[CreateAssetMenu(fileName = "GEGZomBunnyHealth", menuName = "GEG Framework/GEG Property/ZomBunny Health")]
public class GEGZomBunnyHealth : GEGCharacterProperty {
    public GEGZomBunnyHealth() : base("ZomBunnyHealth", 100, 20, true) { }
}
