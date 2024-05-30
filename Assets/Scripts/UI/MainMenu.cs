using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private const int DEFAULT_LEVEL_INDEX = 1;

    [SerializeField] private Button _startButton;
    [SerializeField] private Button _settingsButton;

    [SerializeField] private SettingstWindow _settingstWindow;

    private void OnEnable()
    {
        _startButton.onClick.AddListener(LoadScene);
        _settingsButton.onClick.AddListener(_settingstWindow.Open);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(LoadScene);
        _settingsButton.onClick.RemoveListener(_settingstWindow.Open);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(DEFAULT_LEVEL_INDEX);
    }
}
