using UnityEngine;

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
    }

    private void OnDestroy()
    {
        _finish.Activated -= _winWindow.Open;
    }
}
