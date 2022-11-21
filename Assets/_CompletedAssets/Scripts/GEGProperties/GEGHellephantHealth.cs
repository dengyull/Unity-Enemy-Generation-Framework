using GEGFramework;
using UnityEngine;

[CreateAssetMenu(fileName = "GEGHellephantHealth", menuName = "GEG Framework/GEG Property/Hellephant Health")]
public class GEGHellephantHealth : GEGCharacterProperty {
    public GEGHellephantHealth() : base("HellephantHealth", 100, 20, true) { }
}
