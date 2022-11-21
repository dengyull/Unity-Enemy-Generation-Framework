using GEGFramework;
using UnityEngine;

[CreateAssetMenu(fileName = "GEGZomBearSpeed", menuName = "GEG Framework/GEG Property/ZomBear Speed")]
public class GEGZomBearSpeed : GEGCharacterProperty {
    public GEGZomBearSpeed() : base("ZomBearSpeed", 10, 20, true) { }
}
