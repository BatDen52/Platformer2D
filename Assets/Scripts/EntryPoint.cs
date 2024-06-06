using System.Collections.Generic;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    private const string LEVEL_SCENE_SUBNAME = "Level";

    [SerializeField] private List<string> _sceneNames = new();
    [SerializeField] private SelectLevelWindow _selectLevelWindow;

    private void Awake()
    {
        _selectLevelWindow.SetLevelsNames(_sceneNames);
        SaveService.Initialize(_sceneNames);
    }

    private void Reset()
    {
        int extentionLength = 6;
        _sceneNames.Clear();

        foreach (UnityEditor.EditorBuildSettingsScene scene in UnityEditor.EditorBuildSettings.scenes)
        {
            if (scene.enabled)
            {
                string name = scene.path.Substring(scene.path.LastIndexOf('/') + 1);

                if (name.StartsWith(LEVEL_SCENE_SUBNAME))
                    _sceneNames.Add(name.Substring(0, name.Length - extentionLength));
            }
        }
    }
}