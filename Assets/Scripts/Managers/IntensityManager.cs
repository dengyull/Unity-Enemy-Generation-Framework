using System;
using UnityEngine;

namespace GEGFramework {
    public sealed class IntensityManager : MonoBehaviour {

        public static event Action<float> OnIntensityChanged; // invoked once intensity value changed
        public static IntensityManager Instance { get; private set; } // singleton instance

        [SerializeField, Range(0, 100)]
        float _intensity; // an emotional intensity value ranged from 0 to 100
        public float Intensity { get { return _intensity; } private set { _intensity = value; } }

        [SerializeField] GameMode currentMode = GameMode.Easy;
        public GameMode Mode { get { return currentMode; } }

        [SerializeField, Tooltip("Value (in intensity value) at which intensity value decreases per second")]
        float autoDecreaseAmount = 1f;

        [SerializeField, Tooltip("The cooling down period (in seconds) before intensity value automatically decrease")]
        float autoDecreaseCooldown = 10f;
        float coolDownTimer;

        [SerializeField, Tooltip("Persistence of easy mode (in waves)")]
        int easyModeDuration = 3;

        [SerializeField, Tooltip("Persistence of hard mode (in waves)")]
        int hardModeDuration = 3;

        [SerializeField, Tooltip("The threshold (in intensity value) that triggers the hard mode")]
        float hardModeThreshold = 50;

        int durationCounter; // counts the number of wave in current game mode

        void Awake() {
            // Initialize singleton
            if (Instance != null && Instance != this) Destroy(this);
            else Instance = this;
        }

        void OnEnable() {
            Spawner.OnNewWaveStart += (_) => {
                coolDownTimer = autoDecreaseCooldown;
                UpdateGameMode();
            };
        }

        // Use this for initialization
        void Start() {
            durationCounter = 0;
            coolDownTimer = autoDecreaseCooldown;
            OnIntensityChanged?.Invoke(_intensity);
        }

        // Update is called once per frame
        void Update() {
            coolDownTimer -= Time.deltaTime;
            if (coolDownTimer <= 0) {
                _intensity = Mathf.Clamp(_intensity - autoDecreaseAmount * Time.deltaTime,
                    0, 100);
                OnIntensityChanged?.Invoke(_intensity);
            }
        }

        /// <summary>
        /// Update the intensity value base on special event triggers
        /// </summary>
        /// <param name="percent">(e.g., currentHealth/maxHealth)</param>
        /// <param name="scaler">Scale up/down the [percent] parameter</param>
        /// <param name="proportional">If true, the intensity will increase as [percent] increases</param>
        public void UpdateIntensity(float percent, float scaler, bool proportional) {
            //Debug.Log(String.Format("I:{0} P:{1}, S:{2}, R:{3}", _intensity, percent, scaler, proportional));
            float contribute = percent * scaler;
            if (proportional) _intensity = Mathf.Clamp(_intensity + contribute, 0, 100);
            else _intensity = Mathf.Clamp(_intensity - contribute, 0, 100);
            OnIntensityChanged?.Invoke(_intensity);
            coolDownTimer = autoDecreaseCooldown;
        }

        void UpdateGameMode() {
            durationCounter++;
            if (currentMode == GameMode.Easy && durationCounter > easyModeDuration) {
                // Upon reaching enough waves in easy mode
                durationCounter = 0;
                currentMode = GameMode.Normal;
            } else if (currentMode == GameMode.Normal && _intensity >= hardModeThreshold) {
                // Upon reaching enough intensity value in normal mode
                durationCounter = 0;
                currentMode = GameMode.Hard;
            } else if (currentMode == GameMode.Hard && durationCounter > hardModeDuration) {
                // Upon reaching enough waves in hard mode
                durationCounter = 0;
                currentMode = GameMode.Easy;
            }

            switch (currentMode) {
                case GameMode.Easy:
                    UpdateEnemyQuantity(0, 2);
                    UpdateEnemyQuantity(1, 0);
                    UpdateEnemyQuantity(2, 0);
                    UpdateEnemyProperty("EnemyHealth", 100);
                    UpdateEnemyProperty("EnemySpeed", 4);
                    UpdateEnemyProperty("EnemyDamage", 10);
                    UpdateEnemyProperty("EnemyAttackRate", 0.5f);
                    break;
                case GameMode.Normal:
                    UpdateEnemyQuantity(0, 2);
                    UpdateEnemyQuantity(1, 2);
                    UpdateEnemyQuantity(2, 0);
                    UpdateEnemyProperty("EnemyHealth", 120);
                    UpdateEnemyProperty("EnemySpeed", 6);
                    UpdateEnemyProperty("EnemyDamage", 15);
                    UpdateEnemyProperty("EnemyAttackRate", 0.8f);
                    break;
                case GameMode.Hard:
                    UpdateEnemyQuantity(0, 2);
                    UpdateEnemyQuantity(1, 2);
                    UpdateEnemyQuantity(2, 2);
                    UpdateEnemyProperty("EnemyHealth", 150);
                    UpdateEnemyProperty("EnemySpeed", 8);
                    UpdateEnemyProperty("EnemyDamage", 25);
                    UpdateEnemyProperty("EnemyAttackRate", 1f);
                    break;
            }
        }

        void UpdateEnemyProperty(string propName = null, float? val = null) {
            foreach (GEGCharacter c in PackedData.Instance.characters) {
                if (c.type == CharacterType.Enemy) { // if it's an enemy type
                    foreach (GEGProperty prop in c.properties) {
                        if (prop.enabled && (prop.propertyName == propName || propName == null)) {
                            // if the property enabled for evaluation
                            if (val.HasValue) prop.value = val.Value;
                            else prop.value += prop.value * _intensity / 100 * prop.importance / 100;
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
