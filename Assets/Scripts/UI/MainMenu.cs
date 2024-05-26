using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private const int DEFAULT_LEVEL_INDEX = 1;

    [SerializeField] private Button _startButton;

    private void OnEnable()
    {
        _startButton.onClick.AddListener(LoadScene);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(LoadScene);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(DEFAULT_LEVEL_INDEX);
    }
}
