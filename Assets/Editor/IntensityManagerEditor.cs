using UnityEditor;
using UnityEngine;

namespace GEGFramework {
    [CustomEditor(typeof(IntensityManager))]
    public class IntensityManagerEditor : Editor {
        #region SerializedProperties
        SerializedProperty _intensity;
        SerializedProperty currentMode;
        SerializedProperty autoDecreaseAmount;
        SerializedProperty autoDecreaseCooldown;
        SerializedProperty easyModeDuration;
        SerializedProperty hardModeDuration;
        SerializedProperty hardModeThreshold;
        SerializedProperty expectedFelxibity;
        SerializedProperty expectEasyIntensity;
        SerializedProperty expectNormalIntensity;
        SerializedProperty expectHardIntensity;
        SerializedProperty maxAdjustment;
        #endregion

        IntensityManager _target;

        private void OnEnable() {
            _target = target as IntensityManager;

            _intensity = serializedObject.FindProperty("_intensity");
            currentMode = serializedObject.FindProperty("currentMode");
            autoDecreaseAmount = serializedObject.FindProperty("autoDecreaseAmount");
            autoDecreaseCooldown = serializedObject.FindProperty("autoDecreaseCooldown");
            easyModeDuration = serializedObject.FindProperty("easyModeDuration");
            hardModeDuration = serializedObject.FindProperty("hardModeDuration");
            hardModeThreshold = serializedObject.FindProperty("hardModeThreshold");
            expectedFelxibity = serializedObject.FindProperty("expectedFelxibity");
            expectEasyIntensity = serializedObject.FindProperty("expectEasyIntensity");
            expectNormalIntensity = serializedObject.FindProperty("expectNormalIntensity");
            expectHardIntensity = serializedObject.FindProperty("expectHardIntensity");
            maxAdjustment = serializedObject.FindProperty("maxAdjustment");
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();
            EditorGUILayout.PropertyField(_intensity);
            EditorGUILayout.PropertyField(currentMode);
            EditorGUILayout.PropertyField(autoDecreaseAmount);
            EditorGUILayout.PropertyField(autoDecreaseCooldown);
            EditorGUILayout.PropertyField(expectedFelxibity);
            EditorGUILayout.PropertyField(maxAdjustment);

            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("Duration Settings", EditorStyles.boldLabel);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Easy Mode", GUILayout.Width(70));
            GUILayout.FlexibleSpace();
            EditorGUILayout.PropertyField(easyModeDuration, GUIContent.none, true, GUILayout.MinWidth(70));
            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("Hard Mode", GUILayout.Width(70));
            GUILayout.FlexibleSpace();
            EditorGUILayout.PropertyField(hardModeDuration, GUIContent.none, true, GUILayout.MinWidth(70));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("Easy Mode Settings", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(expectEasyIntensity);

            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("Normal Mode Settings", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(expectNormalIntensity);

            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("Hard Mode Settings", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(hardModeThreshold);
            EditorGUILayout.PropertyField(expectHardIntensity);

            serializedObject.ApplyModifiedProperties();
        }
    }
}