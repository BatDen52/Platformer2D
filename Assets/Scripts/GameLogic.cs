using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Finish _finish;
    [SerializeField] private FailWindow _failWindow;
    [SerializeField] private WinWindow _winWindow;

    private void Awake()
    {
        _failWindow.Initialize(_player);
        _finish.Activated += _winWindow.Open;
        _finish.Activated += UnlockNextLevel;
    }

    private void OnDestroy()
    {
        _finish.Activated -= _winWindow.Open;
        _finish.Activated -= UnlockNextLevel;
    }

    private void UnlockNextLevel()
    {
        SaveService.UnlockNextLevel(SceneManager.GetActiveScene().name);
    }
}
