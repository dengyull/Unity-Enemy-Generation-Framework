using GEGFramework;
using UnityEngine;

[CreateAssetMenu(fileName = "GEGZomBunnySpeed", menuName = "GEG Framework/GEG Property/ZomBunnySpeed")]
public class GEGZomBunnySpeed : GEGCharacterProperty<double> {
    public GEGZomBunnySpeed() : base("ZomBunnySpeed", 10, 20, true) { }
}
