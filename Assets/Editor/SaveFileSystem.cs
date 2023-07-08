using UnityEngine;
using UnityEditor;
using System.IO;

public class SaveFileSystem : EditorWindow
{
    public string saveDataPath;

    Rect TextSection;
    Rect ButtonSection;

    GUISkin SaveSkin;

    [MenuItem("Window/SaveSystem")]
    static void OpenWindow()
    {
        SaveFileSystem window = (SaveFileSystem)GetWindow(typeof(SaveFileSystem));
        window.minSize = new Vector2(375, 200);
        window.maxSize = new Vector2(375, 200);
        window.Show();
    }

    private void OnEnable()
    {
        Init();
        SaveSkin = Resources.Load<GUISkin>("SaveSkin");
    }

    private void OnGUI()
    {
        DrawLayouts();
        DrawText();
    }

    private void DrawLayouts()
    {
        TextSection.x = 0;
        TextSection.y = 0;

        TextSection.width = Screen.width;
        TextSection.height = 150;

        ButtonSection.x = 37.5f;
        ButtonSection.y = 100;

        ButtonSection.width = Screen.width;
        ButtonSection.height = 150;
    }

    private void DrawText()
    {
        GUILayout.BeginArea(TextSection);
        GUILayout.Label("Do you wish to delete the SaveFile?", SaveSkin.GetStyle("Header"));
        GUILayout.EndArea();

        GUILayout.BeginArea(ButtonSection);
        if (GUILayout.Button("Delete", GUILayout.Width(300), GUILayout.Height(50)))
        {
            if (File.Exists(saveDataPath))
            {
                Debug.LogError("Deleted SaveFile");
                DeleteSave();
            }
            else
                Debug.LogError("No SaveFile found");
        }
        GUILayout.EndArea();

    }

    private void Init()
    {
        saveDataPath = SaveDataHandler.FileSearch.filePath;
    }

    public void DeleteSave()
    {
        File.Delete(saveDataPath);
    }
}
