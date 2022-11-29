using UnityEngine;
namespace GEGFramework {
    public class GEGUIManager : MonoBehaviour {
        // The Text UI for updating the difficulty level on the screen
        public TMPro.TMP_Text diffifultyText;
        public TMPro.TMP_Text waveText;

        // Start is called before the first frame update
        void Start() {
            GEGManager.OnDiffChanged += UpdateIntenText;
            GEGManager.OnNewWaveStart += UpdateWaveText;
        }

        // Call this function to update difficulty on UI
        void UpdateIntenText(int currentDiff) {
            diffifultyText.text = "Intensity: " + currentDiff;
        }

        void UpdateWaveText(int waveNum) {
            waveText.text = "Wave: " + waveNum;
        }
    }
}
