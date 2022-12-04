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
        SerializedProperty easyModeIntensityScalar;
        SerializedProperty expectNormalIntensity;
        SerializedProperty normalModeIntensityScalar;
        SerializedProperty expectHardIntensity;
        SerializedProperty hardModeIntensityScalar;
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
            easyModeIntensityScalar = serializedObject.FindProperty("easyModeIntensityScalar");
            expectNormalIntensity = serializedObject.FindProperty("expectNormalIntensity");
            normalModeIntensityScalar = serializedObject.FindProperty("normalModeIntensityScalar");
            expectHardIntensity = serializedObject.FindProperty("expectHardIntensity");
            hardModeIntensityScalar = serializedObject.FindProperty("hardModeIntensityScalar");
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
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Easy Mode Intensity Scalar");
            EditorGUILayout.PropertyField(easyModeIntensityScalar, GUIContent.none, true);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("Normal Mode Settings", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(expectNormalIntensity);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Normal Mode Intensity Scalar");
            EditorGUILayout.PropertyField(normalModeIntensityScalar, GUIContent.none, true);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("Hard Mode Settings", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(hardModeThreshold);
            EditorGUILayout.PropertyField(expectHardIntensity);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Hard Mode Intensity Scalar");
            EditorGUILayout.PropertyField(hardModeIntensityScalar, GUIContent.none, true);
            EditorGUILayout.EndHorizontal();

            serializedObject.ApplyModifiedProperties();
        }
    }
}