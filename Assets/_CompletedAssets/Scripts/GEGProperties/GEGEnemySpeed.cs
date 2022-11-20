using GEGFramework;
using UnityEngine;

[CreateAssetMenu(fileName = "GEGHealth", menuName = "GEG Framework/GEG Property/ZomBear Speed")]
public class GEGZomBearSpeed : GEGCharacterProperty<double> {
    public GEGZomBearSpeed() : base("ZomBear speed", 10, 20, true) { }
}
