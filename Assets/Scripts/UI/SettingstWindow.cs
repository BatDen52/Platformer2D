using UnityEngine;
using UnityEngine.UI;

public class SettingstWindow : MonoBehaviour
{
    [SerializeField] private AudioManager _audioManager;

    [SerializeField] private Button _backButton;
    [SerializeField] private Slider _musicVolume;
    [SerializeField] private Slider _soundVolume;
    [SerializeField] private Toggle _musicSwicther;
    [SerializeField] private Toggle _soundSwicther;

    private void OnEnable()
    {
        _backButton.onClick.AddListener(Close);
        _musicVolume.onValueChanged.AddListener(ChangeVoluemMusic);
        _soundVolume.onValueChanged.AddListener(ChangeVoluemSound);
        _musicSwicther.onValueChanged.AddListener(SwitchMuteMusic);
        _soundSwicther.onValueChanged.AddListener(SwitchMuteSound);
    }

    private void OnDisable()
    {
        _backButton.onClick.RemoveListener(Close);
        _musicVolume.onValueChanged.RemoveListener(ChangeVoluemMusic);
        _soundVolume.onValueChanged.RemoveListener(ChangeVoluemSound);
        _musicSwicther.onValueChanged.RemoveListener(SwitchMuteMusic);
        _soundSwicther.onValueChanged.RemoveListener(SwitchMuteSound);
    }

    public void Open()
    {
        gameObject.SetActive(true);

        _musicSwicther.isOn = PlayerPrefs.GetInt(ConstantsData.SavaData.MUSIC_MUTE_KEY, ConstantsData.SavaData.IS_ON_VALUE) == ConstantsData.SavaData.IS_ON_VALUE;
        _soundSwicther.isOn = PlayerPrefs.GetInt(ConstantsData.SavaData.SOUND_MUTE_KEY, ConstantsData.SavaData.IS_ON_VALUE) == ConstantsData.SavaData.IS_ON_VALUE;
        _musicVolume.value = PlayerPrefs.GetFloat(ConstantsData.SavaData.MUSIC_KEY, ConstantsData.SavaData.DEFAULT_VOLUME);
        _soundVolume.value = PlayerPrefs.GetFloat(ConstantsData.SavaData.SOUND_KEY, ConstantsData.SavaData.DEFAULT_VOLUME);
    }

    private void ChangeVoluemMusic(float value)
    {
        ChangeVoluem(value, ConstantsData.SavaData.MUSIC_KEY);
    }

    private void ChangeVoluemSound(float value)
    {
        ChangeVoluem(value, ConstantsData.SavaData.SOUND_KEY);
    }

    private void ChangeVoluem(float value, string key)
    {
        PlayerPrefs.SetFloat(key, value);
        _audioManager.RefreshSettings();
    }

    private void SwitchMuteMusic(bool isOn)
    {
        SwitchMute(isOn, ConstantsData.SavaData.MUSIC_MUTE_KEY);
    }

    private void SwitchMuteSound(bool isOn)
    {
        SwitchMute(isOn, ConstantsData.SavaData.SOUND_MUTE_KEY);
    }

    private void SwitchMute(bool isOn, string key)
    {
        PlayerPrefs.SetInt(key, isOn ? ConstantsData.SavaData.IS_ON_VALUE : ConstantsData.SavaData.IS_OFF_VALUE);
        _audioManager.RefreshSettings();
    }

    private void Close()
    {
        gameObject.SetActive(false);
    }
}
