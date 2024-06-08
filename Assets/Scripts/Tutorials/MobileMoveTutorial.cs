using System;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class MobileMoveTutorial : MonoBehaviour
{
    [SerializeField] private bool _isMobile;
    [SerializeField] private Image _movePointer;
    [SerializeField] private TouchHandler _joystick;
    [SerializeField] private Image _jumpPointer;
    [SerializeField] private Image _attackPointer;
    [SerializeField] private TutorialTrigger _triggerJump;
    [SerializeField] private TutorialTrigger _triggerDoubleJump;
    [SerializeField] private TutorialTrigger[] _attackTriggers;

    private void Awake()
    {
        if (Application.isEditor && _isMobile == false || YandexGame.EnvironmentData.isDesktop)
            gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if (_joystick != null)
            _joystick.Down += OnDown;

        if (_triggerJump != null)
        {
            _triggerJump.PlayerFounded += ShowJumpPointer;
            _triggerJump.PlayerLosted += HideJumpPointer;
        }

        if (_triggerDoubleJump != null)
        {
            _triggerDoubleJump.PlayerFounded += ShowJumpPointer;
            _triggerDoubleJump.PlayerLosted += HideJumpPointer;
        }

        if (_attackTriggers != null)
        {
            foreach (TutorialTrigger trigger in _attackTriggers)
            {
                trigger.PlayerFounded += ShowAttackPointer;
                trigger.PlayerLosted += HideAttackPointer;
            }
        }
    }

    private void OnDisable()
    {
        if (_joystick != null)
            _joystick.Down -= OnDown;

        if (_triggerJump != null)
        {
            _triggerJump.PlayerFounded -= ShowJumpPointer;
            _triggerJump.PlayerLosted -= HideJumpPointer;
        }

        if (_triggerDoubleJump != null)
        {
            _triggerDoubleJump.PlayerFounded -= ShowJumpPointer;
            _triggerDoubleJump.PlayerLosted -= HideJumpPointer;
        }

        if (_attackTriggers != null)
        {
            foreach (TutorialTrigger trigger in _attackTriggers)
            {
                trigger.PlayerFounded -= ShowAttackPointer;
                trigger.PlayerLosted -= HideAttackPointer;
            }
        }
    }

    private void ShowAttackPointer()
    {
        _attackPointer.gameObject.SetActive(true);
    }

    private void HideAttackPointer()
    {
        _attackPointer.gameObject.SetActive(false);
    }

    private void ShowJumpPointer()
    {
        _jumpPointer.gameObject.SetActive(true);
    }

    private void HideJumpPointer()
    {
        _jumpPointer.gameObject.SetActive(false);
    }

    private void OnDown()
    {
        _joystick.Down -= OnDown;
        _movePointer.gameObject.SetActive(false);
    }
}
