using GEGFramework;
using UnityEngine;

[CreateAssetMenu(fileName = "GEGHellephantSpeed", menuName = "GEG Framework/GEG Property/Hellephant Speed")]
public class GEGHellephantSpeed : GEGCharacterProperty {
    public GEGHellephantSpeed() : base("HellephantSpeed", 10, 20, true) { }
}
