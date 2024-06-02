using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinWindow : PauseWindowBase
{
    [SerializeField] private Button _nextButton;
    private int _nextSceneIndex;

    protected override void OnEnable()
    {
        base.OnEnable();

        _nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        _nextButton.gameObject.SetActive(SceneManager.sceneCountInBuildSettings > _nextSceneIndex);

        _nextButton.onClick.AddListener(LoadNextLevel);
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        _nextButton.onClick.RemoveListener(LoadNextLevel);
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    private void LoadNextLevel()
    {
        LoadScene(_nextSceneIndex);
    }
}
