using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [SerializeField] private AudioManager _audioManager;

    [SerializeField] private AudioClip _stepSound;
    [SerializeField] private AudioClip _jumpSound;
    [SerializeField] private AudioClip _hitSound;
    [SerializeField] private AudioClip _attackSound;
    [SerializeField] private AudioClip _deathSound;

    private float _nextPlayStepTime;

    public void PlayStepSpund()
    {
        if (_nextPlayStepTime < Time.time)
        {
            _nextPlayStepTime = _stepSound.length + Time.time;
            _audioManager.PlayRandomPitchSound(_stepSound);
        }
    }

    public void PlayJumpSpund() => _audioManager.PlaySound(_jumpSound);

    public void PlayHitSpund() => _audioManager.PlaySound(_hitSound);
    
    public void PlayAttackSpund() => _audioManager.PlaySound(_attackSound);

    public void PlayDeathSpund() => _audioManager.PlaySound(_deathSound);
}
