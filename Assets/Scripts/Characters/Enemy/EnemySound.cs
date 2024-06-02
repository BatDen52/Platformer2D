using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySound : MonoBehaviour
{
    [SerializeField] private AudioManager _audioManager;

    [SerializeField] private AudioClip _stepSound;
    [SerializeField] private AudioClip _runSound;
    [SerializeField] private AudioClip _hitSound;
    [SerializeField] private AudioClip _attackSound;
    [SerializeField] private AudioClip _deathSound;

    private float _nextPlayStepTime;
    private float _nextPlayRunTime;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    public void PlayStepSpund()
    {
        if (_audioManager.CanBeHeard(_transform.position))
            if (CanPlaySound(_stepSound, ref _nextPlayStepTime))
                _audioManager.PlayRandomPitchSound(_stepSound);
    }

    public void PlayRunSpund()
    {
        if (_audioManager.CanBeHeard(_transform.position))
            if (CanPlaySound(_runSound, ref _nextPlayRunTime))
                _audioManager.PlaySound(_runSound);
    }

    public void PlayHitSpund() => _audioManager.PlaySound(_hitSound);

    public void PlayAttackSpund() => _audioManager.PlaySound(_attackSound);

    public void PlayDeathSpund() => _audioManager.PlaySound(_deathSound);

    private bool CanPlaySound(AudioClip sound, ref float nextPlayTime)
    {
        if (nextPlayTime < Time.time)
        {
            nextPlayTime = sound.length + Time.time;
            return true;
        }

        return false;
    }
}
