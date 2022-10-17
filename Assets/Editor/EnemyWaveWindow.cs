using UnityEngine;
using UnityEditor;

public class EnemyWaveWindow : EditorWindow {

    [MenuItem("Window/Enemy Wave Framework")]
    public static void ShowWindow() {
        EnemyWaveWindow window = GetWindow<EnemyWaveWindow>("Enemy Wave Framework");
        window.titleContent = new GUIContent("Enemy Wave Framework");
        window.Show();
    }

    private void OnGUI() {
        // window contents
    }
}