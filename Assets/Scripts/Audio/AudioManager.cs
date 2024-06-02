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
        _musicSource.mute = PlayerPrefs.GetInt(ConstantsData.SavaData.MUSIC_MUTE_KEY, ConstantsData.SavaData.IS_ON_VALUE) == ConstantsData.SavaData.IS_OFF_VALUE;
        
        _soundSource.mute = PlayerPrefs.GetInt(ConstantsData.SavaData.SOUND_MUTE_KEY, ConstantsData.SavaData.IS_ON_VALUE) == ConstantsData.SavaData.IS_OFF_VALUE;
        _randomPitchSoundSource.mute = PlayerPrefs.GetInt(ConstantsData.SavaData.SOUND_MUTE_KEY, ConstantsData.SavaData.IS_ON_VALUE) == ConstantsData.SavaData.IS_OFF_VALUE;
      
        _musicSource.volume = PlayerPrefs.GetFloat(ConstantsData.SavaData.MUSIC_KEY, ConstantsData.SavaData.DEFAULT_VOLUME);
        
        _soundSource.volume = PlayerPrefs.GetFloat(ConstantsData.SavaData.SOUND_KEY, ConstantsData.SavaData.DEFAULT_VOLUME);
        _randomPitchSoundSource.volume = PlayerPrefs.GetFloat(ConstantsData.SavaData.SOUND_KEY, ConstantsData.SavaData.DEFAULT_VOLUME);
    }
}
