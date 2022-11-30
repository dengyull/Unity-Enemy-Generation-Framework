using UnityEngine;
using TMPro;

namespace CompleteProject {
    public class ScoreManager : MonoBehaviour {
        public static int score;        // The player's score.
        TextMeshProUGUI text;           // Reference to the Text component.

        void Awake() {
            // Set up the reference.
            text = GetComponent<TextMeshProUGUI>();

            // Reset the score.
            score = 0;
        }

        void Update() {
            // Set the displayed text to be the word "Score" followed by the score value.
            text.text = "Score: " + score;
        }
    }
}