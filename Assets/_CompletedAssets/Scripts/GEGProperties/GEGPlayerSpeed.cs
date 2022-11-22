 using GEGFramework;
using UnityEngine;

[CreateAssetMenu(fileName = "GEGPlayerSpeed", menuName = "GEG Framework/GEG Property/Player Speed")]
public class GEGPlayerSpeed : GEGCharacterProperty {
    public GEGPlayerSpeed() : base("PlayerSpeed", 6, 20, true) { }
}
