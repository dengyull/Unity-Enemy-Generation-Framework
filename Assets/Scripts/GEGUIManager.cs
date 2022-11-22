using UnityEngine;
namespace GEGFramework {
    public class GEGUIManager : MonoBehaviour {
        // The Text UI for updating the difficulty level on the screen
        public TMPro.TMP_Text diffifultyText;
        public TMPro.TMP_Text waveText;

        public int waveNum;

        // Start is called before the first frame update
        void Start() {
            GEGManager.OnDiffChanged += UpdateDiffText;
            diffifultyText.text = "Default Difficulty";
            waveNum = 0;
            waveText.text = "Wave: "+waveNum;
        }

        // Call this function to update difficulty on UI
        void UpdateDiffText(int currentDiff) {
            waveNum++;
            diffifultyText.text = "Difficulty Level: " + currentDiff;
            waveText.text = "Wave: " + waveNum;
        }
    }
}
