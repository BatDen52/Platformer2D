using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class PlayerInputInitializer : MonoBehaviour
{
    [SerializeField] private bool _isMobile;
    [SerializeField] private Player _player;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private MobileInputReader _mobileInput;
    [SerializeField] private TMP_Text _interactInput;
    [SerializeField] private Image _interactMobileInput;

    private void Awake()
    {
        if (Application.isEditor && _isMobile || YandexGame.EnvironmentData.isMobile || YandexGame.EnvironmentData.isTablet)
        {
            _player.Initialize(_mobileInput);
            _inputReader.enabled = false;
            _interactMobileInput.gameObject.SetActive(true);
            _interactInput.gameObject.SetActive(false);
        }
        else
        {
            _player.Initialize(_inputReader);
            _mobileInput.gameObject.SetActive(false);
            _interactMobileInput.gameObject.SetActive(false);
            _interactInput.gameObject.SetActive(true);
        }
    }
}
