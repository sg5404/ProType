using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class EditorMenu : MonoBehaviour
{
    [MenuItem("ProjectM/Scenes/Main")]
    static void EditorMenu_MainScene()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/MainScene.unity");
    }

    [MenuItem("ProjectM/Scenes/Loading")]
    static void EditorMenu_LoadingScene()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/LoadingScene.unity");
    }
}
