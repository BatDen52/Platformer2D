public class FailWindow : PauseWindowBase
{
    private Player _player;

    private void OnDestroy()
    {
        _player.Died -= OnPlayerDied;
    }

    public void Initialize(Player player)
    {
        _player = player;
        _player.Died += OnPlayerDied;
    }

    private void OnPlayerDied()
    {
        gameObject.SetActive(true);
    }
}
