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

        _musicSwicther.isOn = SaveService.MusicIsOn;
        _soundSwicther.isOn = SaveService.SoundIsOn;
        _musicVolume.value = SaveService.MusicVolume;
        _soundVolume.value = SaveService.SoundVolume;
    }

    private void ChangeVoluemMusic(float value)
    {
        SaveService.SetMusicVolume(value);
        _audioManager.RefreshSettings();
    }

    private void ChangeVoluemSound(float value)
    {
        SaveService.SetSoundVolume(value);
        _audioManager.RefreshSettings();
    }

    private void SwitchMuteMusic(bool isOn)
    {
        SaveService.SetMusicIsOn(isOn);
        _audioManager.RefreshSettings();
    }

    private void SwitchMuteSound(bool isOn)
    {
        SaveService.SetSoundIsOn(isOn);
        _audioManager.RefreshSettings();
    }

    private void Close()
    {
        gameObject.SetActive(false);
        SaveService.Save();
    }
}
