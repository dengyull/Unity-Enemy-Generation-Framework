using System;
using UnityEngine;

namespace GEGFramework {
    public class IntensityManager : MonoBehaviour {

        public static event Action<float> OnIntensityChanged; // invoked once intensity value changed

        [SerializeField] GameMode currentMode = GameMode.Easy;

        float _intensity; // an emotional intensity value ranged from 0 to 100
        float Intensity { get { return _intensity; } }

        [SerializeField, Tooltip("Value (in intensity value) at which intensity value decreases per second")]
        float autoDecreaseValue = 1f;

        [SerializeField, Tooltip("The cooling down period (in seconds) before intensity value automatically decrease")]
        float autoDecreaseCooldown = 10f;

        [SerializeField, Tooltip("Persistence of easy mode (in waves)")]
        int easyModeDuration = 3;

        [SerializeField, Tooltip("Persistence of hard mode (in waves)")]
        int hardModeDuration = 3;

        [SerializeField, Tooltip("The threshold (in intensity value) that triggers the hard mode")]
        float hardModeThreshold = 50;

        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {
            if (Input.GetKey(KeyCode.Q)) {
                _intensity++;
                OnIntensityChanged?.Invoke(Intensity);
            }
        }

        void Generate() {

        }
    }
}