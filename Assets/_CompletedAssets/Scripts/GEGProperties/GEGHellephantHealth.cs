using GEGFramework;
using UnityEngine;

[CreateAssetMenu(fileName = "GEGHellephantHealth", menuName = "GEG Framework/GEG Property/HellephantHealth")]
public class GEGHellephantHealth : GEGCharacterProperty<double> {
    public GEGHellephantHealth() : base("HellephantHealth", 100, 20, true) { }
}
