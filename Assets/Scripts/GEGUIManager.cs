using UnityEngine;
using TMPro;

namespace GEGFramework {
    public class GEGUIManager : MonoBehaviour {
        // The Text UI for updating the difficulty level on the screen
        public TMP_Text intensityText;
        public TMP_Text waveText;

        // Start is called before the first frame update
        void Start() {
            GEGManager.OnNewWaveStart += UpdateWaveText;
        }

        // Call this function to update difficulty on UI
        void UpdateIntenText(int currentInten) {
            intensityText.text = "Intensity: " + currentInten;
        }

        void UpdateWaveText(int waveNum) {
            waveText.text = "Wave: " + waveNum;
        }
    }
}
