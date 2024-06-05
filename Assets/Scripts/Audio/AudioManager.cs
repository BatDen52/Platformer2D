using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioListener _listener;

    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _soundSource;
    [SerializeField] private AudioSource _randomPitchSoundSource;

    [SerializeField] private AudioClip _defaultMusic;

    [SerializeField] private float _lowPitch = 0f;
    [SerializeField] private float _topPitch = 2f;

    [SerializeField] private float _sqrMaxDistanceToSorce = 100f;

    private Transform _listenerTransform;

    private void Awake()
    {
        _listenerTransform = _listener.transform;

        RefreshSettings();

        PlayMusic(_defaultMusic);
        _musicSource.loop = true;

        _soundSource.playOnAwake = false;
        _soundSource.loop = false;
    }

    public bool CanBeHeard(Vector3 sourcePosition)
        => (sourcePosition - _listenerTransform.position).sqrMagnitude < _sqrMaxDistanceToSorce;

    public void PlayMusic(AudioClip clip)
    {
        _musicSource.Stop();
        _musicSource.clip = clip;
        _musicSource.Play();
    }

    public void PlaySound(AudioClip clip)
    {
        _soundSource.PlayOneShot(clip);
    }

    public void PlayRandomPitchSound(AudioClip clip)
    {
        _randomPitchSoundSource.pitch = Random.Range(_lowPitch, _topPitch);
        _randomPitchSoundSource.PlayOneShot(clip);
    }

    public void RefreshSettings()
    {
        _musicSource.mute = SaveService.MusicIsOn == false;

        _soundSource.mute = SaveService.SoundIsOn == false;
        _randomPitchSoundSource.mute = SaveService.SoundIsOn == false;

        _musicSource.volume = SaveService.MusicVolume;

        _soundSource.volume = SaveService.SoundVolume;
        _randomPitchSoundSource.volume = SaveService.SoundVolume;
    }
}
