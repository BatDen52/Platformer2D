using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectLevelWindow : MonoBehaviour
{
    private const string LEVEL_SCENE_SUBNAME = "Level";

    [SerializeField] private LevelCell _cellPrefub;
    [SerializeField] private RectTransform _container;
    [SerializeField] private Button _backButton;
    [SerializeField] private List<string> _sceneNames = new();

    private List<LevelCell> _levelCells = new();

    private void OnEnable()
    {
        _backButton.onClick.AddListener(Close);
        FillLevels();
    }

    private void OnDisable()
    {
        _backButton.onClick.RemoveListener(Close);
        ClearLevels();
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

    private void FillLevels()
    {
        LevelCell cell;
        int levelNumber = 1;

        foreach (string sceneName in _sceneNames)
        {
            cell = Instantiate(_cellPrefub, _container);
            cell.Initialize(sceneName, levelNumber, true);
            cell.SceneSelected += OnSceneSelected;

            _levelCells.Add(cell);

            levelNumber++;
        }
    }

    private void ClearLevels()
    {
        foreach (LevelCell cell in _levelCells)
        {
            cell.SceneSelected -= OnSceneSelected;
            Destroy(cell.gameObject);
        }

        _levelCells.Clear();
    }

    private void OnSceneSelected(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    private void Close()
    {
        gameObject.SetActive(false);
    }
}
