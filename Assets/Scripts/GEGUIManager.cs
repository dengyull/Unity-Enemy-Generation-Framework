using UnityEngine;
namespace GEGFramework
{
    public class GEGUIManager : MonoBehaviour
    {
        // The Text UI for updating the difficulty level on the screen
        public TMPro.TMP_Text diffifultyText;


        // Start is called before the first frame update
        void Start()
        {
            diffifultyText.text = "Default Difficulty";
        }

        // Call this function to update difficulty on UI
        void UpdateDiffText(int currentDiff)
        {
            diffifultyText.text = "Difficulty Level: " + currentDiff;
        }
    }
}
