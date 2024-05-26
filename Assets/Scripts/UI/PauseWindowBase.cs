using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public abstract class PauseWindowBase : MonoBehaviour
{
    private const int MAIN_MENU_SCENE_INDEX = 0;

    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;

    protected virtual void OnEnable()
    {
        TimeManager.Pause();

        _restartButton.onClick.AddListener(Restart);
        _exitButton.onClick.AddListener(Exit);
    }

    protected virtual void OnDisable()
    {
        TimeManager.Run();

        _restartButton.onClick.RemoveListener(Restart);
        _exitButton.onClick.RemoveListener(Exit);
    }

    private void Restart()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Exit()
    {
        LoadScene(MAIN_MENU_SCENE_INDEX);
    }

    private void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
