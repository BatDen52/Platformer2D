using System;
using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(GroundDetector), typeof(Mover))]
[RequireComponent(typeof(PlayerAnimator), typeof(CollisionHandler), typeof(PlayerAttacker))]
[RequireComponent(typeof(PlayerSound))]
public class Player : Character
{
    [SerializeField] private PlayerAnimationEvent _animationEvent;

    private InputReader _inputReader;
    private GroundDetector _groundDetector;
    private Mover _mover;
    private PlayerAnimator _animator;
    private PlayerAttacker _attacker;
    private CollisionHandler _collisionHandler;
    private PlayerSound _audio;

    private IInteractable _interactable;

    protected override void Awake()
    {
        base.Awake();

        _groundDetector = GetComponent<GroundDetector>();
        _inputReader = GetComponent<InputReader>();
        _mover = GetComponent<Mover>();
        _animator = GetComponent<PlayerAnimator>();
        _attacker = GetComponent<PlayerAttacker>();
        _collisionHandler = GetComponent<CollisionHandler>();
        _audio = GetComponent<PlayerSound>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        _collisionHandler.FinishReached += OnFinishReached;
        _animationEvent.AttackStarted += OnAttackStarted;
        _animationEvent.AttackEnded += OnAttackEnded;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        _collisionHandler.FinishReached -= OnFinishReached;
        _animationEvent.AttackStarted -= OnAttackStarted;
        _animationEvent.AttackEnded -= OnAttackEnded;
    }

    private void FixedUpdate()
    {
        if (TimeManager.IsPaused)
            return;

        _animator.SetSpeedX(_inputReader.Direction);

        if (_inputReader.Direction != 0)
        {
            _mover.Move(_inputReader.Direction);
            Fliper.LookAtTarget(transform.position + Vector3.right * _inputReader.Direction);

            if (_groundDetector.IsGround)
                _audio.PlayStepSpund();
        }

        if (_inputReader.GetIsJump() && _groundDetector.IsGround)
        {
            _mover.Jump();
            _audio.PlayJumpSpund();
        }

        if (_inputReader.GetIsAttack() && _attacker.CanAttack)
        {
            _attacker.PrepareAttack();
            _animator.SetAttackTrigger();
            _audio.PlayAttackSpund();
        }

        if (_inputReader.GetIsInteract() && _interactable != null)
            _interactable.Interact();
    }

    protected override void OnTakingDamage()
    {
        _animator.SetHitTrigger();
        _audio.PlayHitSpund();
    }

    protected override void OnDied()
    {
        base.OnDied();
        _audio.PlayDeathSpund();
    }

    private void OnFinishReached(IInteractable interactable)
    {
        _interactable = interactable;
    }

    private void OnAttackEnded()
    {
        _attacker.StopAttack();
    }

    private void OnAttackStarted()
    {
        _attacker.StartAttack();
    }
}
