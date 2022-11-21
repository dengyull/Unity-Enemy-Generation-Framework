 using GEGFramework;
using UnityEngine;

[CreateAssetMenu(fileName = "GEGPlayerSpeed", menuName = "GEG Framework/GEG Property/PlayerSpeed")]
public class GEGPlayerSpeed : GEGCharacterProperty<double> {
    public GEGPlayerSpeed() : base("PlayerSpeed", 6, 20, true) { }
}
