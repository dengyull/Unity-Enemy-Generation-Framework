using System;
using UnityEngine;

namespace GEGFramework {
    public sealed class IntensityManager : MonoBehaviour {

        public static event Action<float> OnIntensityChanged; // invoked once intensity value changed
        public static IntensityManager Instance { get; private set; } // singleton instance

        [SerializeField, Range(0, 100), Tooltip("Emotional intensity of player")]
        float _intensity; // an emotional intensity value ranged from 0 to 100
        public float Intensity { get { return _intensity; } private set { _intensity = value; } }

        [SerializeField] GameMode currentMode = GameMode.Easy;
        public GameMode Mode { get { return currentMode; } }

        [SerializeField, Tooltip("Value (in intensity value) at which intensity value decreases per second")]
        float autoDecreaseAmount;

        [SerializeField, Tooltip("The cooling down period (in seconds) before intensity value " +
            "automatically decrease")]
        float autoDecreaseCooldown;

        [SerializeField, Tooltip("Persistence of easy mode (in waves)")]
        int easyModeDuration;

        [SerializeField, Tooltip("Persistence of hard mode (in waves)")]
        int hardModeDuration;

        [SerializeField, Tooltip("The threshold (in intensity value) that triggers the hard mode")]
        float hardModeThreshold;

        [SerializeField, Range(1, 100), Tooltip("Maximum amount of adjustment (in percentage) for each property" +
            "when difficulty adjustment happens")]
        float maxAdjustment;

        [SerializeField, Range(0, 100), Tooltip("Flexibility of expected intensity; Expected intensity range " +
            "= [expectIntensity - expectedFelxibity, expectIntensity + expectedFelxibity]")]
        float expectedFelxibity; // [expectIntensity - expectedFelxibity, expectIntensity + expectedFelxibity]

        [SerializeField, Range(0, 100), Tooltip("Expected emotional intensity of easy mode")]
        float expectEasyIntensity;
        [SerializeField, Tooltip("Scale up/down the intensity adjustment in easy mode")]
        float easyModeIntensityScalar;

        [SerializeField, Range(0, 100), Tooltip("Expected emotional intensity of normal mode")]
        float expectNormalIntensity;
        [SerializeField, Tooltip("Scale up/down the intensity adjustments in normal mode")]
        float normalModeIntensityScalar;

        [SerializeField, Range(0, 100), Tooltip("Expected emotional intensity of hard mode")]
        float expectHardIntensity;
        [SerializeField, Tooltip("Scale up/down the intensity adjustments in hard mode")]
        float hardModeIntensityScalar;

        float coolDownTimer;
        int durationCounter; // counts the number of wave in current game mode

        void Awake() {
            // Initialize singleton
            if (Instance != null && Instance != this) Destroy(this);
            else Instance = this;

            durationCounter = 0;
            coolDownTimer = autoDecreaseCooldown;
            OnIntensityChanged?.Invoke(_intensity);
        }

        void OnEnable() {
            Spawner.OnNewWaveStart += (_) => {
                coolDownTimer = autoDecreaseCooldown;
                UpdateGameMode(); // Udpate game mode when new wave starts
            };
        }

        // Update is called once per frame
        void Update() {
            coolDownTimer -= Time.deltaTime;
            if (coolDownTimer <= 0) {
                switch (currentMode) {
                    case GameMode.Easy:
                        _intensity = Mathf.Clamp(_intensity - autoDecreaseAmount * easyModeIntensityScalar
                            * Time.deltaTime, 0, 100);
                        break;
                    case GameMode.Normal:
                        _intensity = Mathf.Clamp(_intensity - autoDecreaseAmount * normalModeIntensityScalar
                            * Time.deltaTime, 0, 100);
                        break;
                    case GameMode.Hard:
                        _intensity = Mathf.Clamp(_intensity - autoDecreaseAmount * hardModeIntensityScalar
                            * Time.deltaTime, 0, 100);
                        break;
                }
                OnIntensityChanged?.Invoke(_intensity);
            }
        }

        void OnApplicationQuit() {
            foreach (GEGCharacter c in PackedData.Instance.characters) {
                foreach (GEGProperty prop in c.properties) {
                    prop.value = prop.defaultValue; // reset scriptable objects
                }
            }
        }

        /// <summary>
        /// Update the intensity value base on special event triggers
        /// </summary>
        /// <param name="percent">(e.g., currentHealth/maxHealth)</param>
        /// <param name="scalar">Scale up/down the [percent] parameter</param>
        /// <param name="increase">If true, the intensity will increase as [percent] increases</param>
        public void UpdateIntensity(float percent, float scalar, bool increase) {
            float contribute = 0;
            switch (currentMode) {
                case GameMode.Easy:
                    contribute = percent * scalar * easyModeIntensityScalar;
                    break;
                case GameMode.Normal:
                    contribute = percent * scalar * normalModeIntensityScalar;
                    break;
                case GameMode.Hard:
                    contribute = percent * scalar * hardModeIntensityScalar;
                    break;
            }
            if (increase) _intensity = Mathf.Clamp(_intensity + contribute, 0, 100);
            else _intensity = Mathf.Clamp(_intensity - contribute, 0, 100);
            OnIntensityChanged?.Invoke(_intensity);
            coolDownTimer = autoDecreaseCooldown;
        }

        void UpdateGameMode() {
            durationCounter++;
            switch (currentMode) {
                case GameMode.Easy:
                    if (durationCounter > easyModeDuration) {
                        durationCounter = 0;
                        currentMode = GameMode.Normal;
                    }
                    if (_intensity > expectEasyIntensity + expectedFelxibity) { // relax mode is too hard
                        UpdateAllEnemyProperty(false, maxAdjustment, easyModeIntensityScalar);
                    } else if (_intensity < expectEasyIntensity - expectedFelxibity) { // relax mode is too easy
                        UpdateAllEnemyProperty(true, maxAdjustment, easyModeIntensityScalar);
                    } // else within expect intensity
                    UpdateEnemyQuantity(0, 2);
                    UpdateEnemyQuantity(1, 3);
                    UpdateEnemyQuantity(2, 0);
                    break;
                case GameMode.Normal:
                    if (_intensity >= hardModeThreshold) {
                        durationCounter = 0;
                        currentMode = GameMode.Hard;
                    }
                    if (_intensity > expectNormalIntensity + expectedFelxibity) { // normal mode is too hard
                        UpdateAllEnemyProperty(false, maxAdjustment, normalModeIntensityScalar);
                    } else if (_intensity < expectNormalIntensity - expectedFelxibity) { // normal mode is too easy
                        UpdateAllEnemyProperty(true, maxAdjustment, normalModeIntensityScalar);
                    } // else within expect intensity
                    UpdateEnemyQuantity(0, 5);
                    UpdateEnemyQuantity(1, 5);
                    UpdateEnemyQuantity(2, 2);
                    break;
                case GameMode.Hard:
                    if (durationCounter > hardModeDuration) {
                        durationCounter = 0;
                        currentMode = GameMode.Easy;
                    }
                    if (_intensity > expectHardIntensity + expectedFelxibity) { // hard mode is too hard
                        UpdateAllEnemyProperty(false, maxAdjustment, hardModeIntensityScalar);
                    } else if (_intensity < expectHardIntensity - expectedFelxibity) { // hard mode is too easy
                        UpdateAllEnemyProperty(true, maxAdjustment, hardModeIntensityScalar);
                    } // else within expect intensity
                    UpdateEnemyQuantity(0, 8);
                    UpdateEnemyQuantity(1, 7);
                    UpdateEnemyQuantity(2, 5);
                    break;
            }
        }

        void UpdateAllEnemyProperty(bool increase, float percent, float? scaler = null) {
            foreach (GEGCharacter c in PackedData.Instance.characters) {
                if (c.type == CharacterType.Enemy) { // if it's an enemy type
                    foreach (GEGProperty prop in c.properties) {
                        if (prop.enabled) { // if the property enabled for evaluation
                            float adjustment = prop.defaultValue * (percent / 100) * (prop.importance / 100);
                            prop.value += increase ? adjustment : -adjustment; // update property's value
                        }
                    }
                }
            }
        }

        void UpdateEnemyQuantity(int index, int quantity) {
            if (index < PackedData.Instance.characters.Count) {
                if (PackedData.Instance.characters[index].type == CharacterType.Enemy)
                    PackedData.Instance.characters[index].numNextWave = quantity;
            }
        }
    }
}
