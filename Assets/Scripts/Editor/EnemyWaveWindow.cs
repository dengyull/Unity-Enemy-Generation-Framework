using UnityEngine;
using UnityEditor;


public class EnemyWaveWindow : EditorWindow {

    [MenuItem("Window/Enemy Wave Framework")]
    private static void ShowWindow() {
        var window = GetWindow<EnemyWaveWindow>("Enemy Wave Framework");
        window.titleContent = new GUIContent("EnemyWaveWindow");
        window.Show();
    }

    private void OnGUI() {
        
        if (GUILayout.Button("Place Spawn Point(s)")) {
            Debug.Log("Place Spawn Point(s) clicked");
        }
    }
}