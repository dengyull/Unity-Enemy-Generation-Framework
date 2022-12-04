using UnityEngine;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GEGFramework {
    public class UIManager : MonoBehaviour {
        // The Text UI for updating the difficulty level on the screen

        [SerializeField] GameObject debugPrefab;
        [SerializeField] bool debugEnabled;
        [SerializeField] TMP_Text modeText;
        [SerializeField] TMP_Text waveText;
        [SerializeField] TMP_Text intensityText;

        // Start is called before the first frame update
        void Start() {
            // Update wave number in debug UI:
            Spawner.OnNewWaveStart += (int waveNum) => {
                waveText.text = "Mode: " + IntensityManager.Instance.Mode;
                waveText.text = "Wave: " + waveNum;
            };
            // Update intensity value in debug UI:
            IntensityManager.OnIntensityChanged += (float intensity) => intensityText.text = "Intensity: "
                + Mathf.RoundToInt(intensity);
        }

        void Update() {
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies) {
                var debugInst = enemy.transform.Find(debugPrefab.name);
                debugInst.gameObject.SetActive(debugEnabled);
            }
        }

        public void ToggleDebugUI() {
            if (debugEnabled) {
                waveText.enabled = true;
                intensityText.enabled = true;
            } else {
                waveText.enabled = false;
                intensityText.enabled = false;
            }
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(UIManager))]
    public class UIManagerEditor : Editor {
        #region SerializedProperties
        SerializedProperty debugPrefab;
        SerializedProperty debugEnabled;
        SerializedProperty intensityText;
        SerializedProperty waveText;
        #endregion

        UIManager manager;

        private void OnEnable() {
            manager = target as UIManager;
            debugPrefab = serializedObject.FindProperty("debugPrefab");
            debugEnabled = serializedObject.FindProperty("debugEnabled");
            intensityText = serializedObject.FindProperty("intensityText");
            waveText = serializedObject.FindProperty("waveText");
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();
            EditorGUILayout.PropertyField(debugPrefab);
            debugEnabled.boolValue = EditorGUILayout.Toggle("Debug UI", debugEnabled.boolValue); // toggle field for debugUI
            if (debugEnabled.boolValue) { // if debug UI is enabled, show:
                EditorGUILayout.PropertyField(intensityText);
                EditorGUILayout.PropertyField(waveText);
            }
            serializedObject.ApplyModifiedProperties();
            manager.ToggleDebugUI();
        }
    }
#endif
}
