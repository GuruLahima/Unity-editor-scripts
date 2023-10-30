using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using System.Reflection;
using UnityEngine.SceneManagement;
using System;

namespace GuruLaghima.EditorTools
{
  [InitializeOnLoad]
  public class SceneNameBannerEditorTool : EditorWindow
  {
    private static string newActiveScene;

    static SceneNameBannerEditorTool()
    {
      // show the scene name as soon as this script is loaded
      SceneView.duringSceneGui -= OnSceneGUI;
      SceneView.duringSceneGui += OnSceneGUI;

      newActiveScene = EditorSceneManager.GetActiveScene().name;

      // show the scene name every time a new scene is loaded/reloaded
      EditorSceneManager.activeSceneChangedInEditMode += ShowSceneNameBanner;
    }

    private static void ShowSceneNameBanner(Scene arg0, Scene arg1)
    {
      newActiveScene = arg1.name;

      SceneView.duringSceneGui -= OnSceneGUI;
      SceneView.duringSceneGui += OnSceneGUI;

    }

    [MenuItem("Window/Custom Tools/SceneNameBannerEditorTool Enable")]
    public static void Enable()
    {
      SceneView.duringSceneGui -= OnSceneGUI;
      SceneView.duringSceneGui += OnSceneGUI;
    }

    [MenuItem("Window/Custom Tools/SceneNameBannerEditorTool Disable")]
    public static void Disable()
    {
      SceneView.duringSceneGui -= OnSceneGUI;
    }

    private static void OnSceneGUI(SceneView sceneview)
    {
      Handles.BeginGUI();

      GUILayout.BeginArea(new Rect(10, 10, Screen.width - 20, Screen.height - 65));

      GUIContent gUI = new GUIContent
      {
        text = newActiveScene
      };
      GUIStyle buttonStyle = new GUIStyle(GUI.skin.button)
      {
        fontSize = Screen.height / 4,
        alignment = TextAnchor.MiddleCenter,
        wordWrap = true,
        border = new RectOffset(0, 0, 0, 0),
        imagePosition = ImagePosition.TextOnly,
        stretchWidth = true,
        stretchHeight = true
      };
      Color tempCol = GUI.backgroundColor;
      tempCol.a = 0.5f;
      GUI.backgroundColor = tempCol;
      if (GUILayout.Button(newActiveScene, buttonStyle))
        SceneView.duringSceneGui -= OnSceneGUI;

      GUILayout.EndArea();


      Handles.EndGUI();
    }

  }
}