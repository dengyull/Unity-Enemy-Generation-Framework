using GEGFramework;
using UnityEngine;

[CreateAssetMenu(fileName = "GEGHellephantSpeed", menuName = "GEG Framework/GEG Property/HellephantSpeed")]
public class GEGHellephantSpeed : GEGCharacterProperty<double> {
    public GEGHellephantSpeed() : base("HellephantSpeed", 10, 20, true) { }
}
