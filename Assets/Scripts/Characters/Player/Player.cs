using System;
using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(GroundDetector), typeof(Mover))]
[RequireComponent(typeof(PlayerAnimator), typeof(CollisionHandler), typeof(PlayerAttacker))]
public class Player : Character
{
    [SerializeField] private PlayerAnimationEvent _animationEvent;

    private InputReader _inputReader;
    private GroundDetector _groundDetector;
    private Mover _mover;
    private PlayerAnimator _animator;
    private PlayerAttacker _attacker;
    private CollisionHandler _collisionHandler;

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
        }

        if (_inputReader.GetIsJump() && _groundDetector.IsGround)
            _mover.Jump();

        if (_inputReader.GetIsAttack() && _attacker.CanAttack)
        {
            _attacker.PrepareAttack();
            _animator.SetAttackTrigger();
        }

        if (_inputReader.GetIsInteract() && _interactable != null)
            _interactable.Interact();
    }

    protected override void OnTakingDamage()
    {
        _animator.SetHitTrigger();
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
