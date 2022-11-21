using GEGFramework;
using UnityEngine;

[CreateAssetMenu(fileName = "GEGZomBearSpeed", menuName = "GEG Framework/GEG Property/ZomBearSpeed")]
public class GEGZomBearSpeed : GEGCharacterProperty<double> {
    public GEGZomBearSpeed() : base("ZomBearSpeed", 10, 20, true) { }
}
