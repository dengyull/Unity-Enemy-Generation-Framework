using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GEGFramework {
    public class PackedDataMonitor : MonoBehaviour {
        public PackedData packedData = PackedData.Instance;
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(PackedDataMonitor))]
    public class PackedDataMonitorEditor : Editor {
        #region SerializedProperties
        SerializedProperty packedData;
        #endregion

        PackedDataMonitor _target;

        private void OnEnable() {
            _target = target as PackedDataMonitor;
            packedData = serializedObject.FindProperty("packedData");
        }

        public override void OnInspectorGUI() {
            DrawDefaultInspector();
            if (GUI.changed && !Application.isPlaying) {
                EditorUtility.SetDirty(_target);
            }
        }
    }
#endif
}