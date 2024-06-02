using System;
using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(PlayerSound), typeof(Mover))]
[RequireComponent(typeof(PlayerAnimator), typeof(CollisionHandler), typeof(PlayerAttacker))]
public class Player : Character
{
    [SerializeField] private PlayerAnimationEvent _animationEvent;
    [SerializeField] private Canvas _interactableCanvas;
    [SerializeField] private InventoryView _inventoryView;
    [SerializeField] private GroundDetector _groundDetector;

    private InputReader _inputReader;
    private Mover _mover;
    private PlayerAnimator _animator;
    private PlayerAttacker _attacker;
    private CollisionHandler _collisionHandler;
    private PlayerSound _audio;

    private Inventory _inventory;

    private IInteractable _interactable;

    protected override void Awake()
    {
        base.Awake();

        _inputReader = GetComponent<InputReader>();
        _mover = GetComponent<Mover>();
        _animator = GetComponent<PlayerAnimator>();
        _attacker = GetComponent<PlayerAttacker>();
        _collisionHandler = GetComponent<CollisionHandler>();
        _audio = GetComponent<PlayerSound>();

        _inventory = new Inventory();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        _collisionHandler.InteractableFounded += OnInteractableFounded;
        _collisionHandler.MedKitFounded += OnMedKitFounded;
        _collisionHandler.KeyFounded += OnKeyFounded;
        _animationEvent.AttackStarted += OnAttackStarted;
        _animationEvent.AttackEnded += OnAttackEnded;

        _inventory.ItemAdded += AddItemToInventory;
        _inventory.ItemRemoved += _inventoryView.Remove;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        _collisionHandler.InteractableFounded -= OnInteractableFounded;
        _collisionHandler.MedKitFounded -= OnMedKitFounded;
        _collisionHandler.KeyFounded -= OnKeyFounded;
        _animationEvent.AttackStarted -= OnAttackStarted;
        _animationEvent.AttackEnded -= OnAttackEnded;

        _inventory.ItemAdded -= AddItemToInventory;
        _inventory.ItemRemoved -= _inventoryView.Remove;
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
        {
            if (_interactable.IsLock)
            {
                if (_inventory.Contains(_interactable.Key))
                {
                    _interactable.Unlock((Key)_inventory.Take(_interactable.Key));
                }
            }
            else
            {
                _interactable.Interact();
            }
        }
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

    private void OnInteractableFounded(IInteractable interactable)
    {
        _interactable = interactable;
        _interactableCanvas.gameObject.SetActive(interactable != null);
    }

    private void OnMedKitFounded(MedKit medKit)
    {
        if(Health.Value < Health.MaxValue)
        {
            Heal(medKit.Value);
            medKit.Collect();
        }
    }

    private void OnKeyFounded(Key key)
    {
        _inventory.Add(key);
    }

    private void OnAttackEnded()
    {
        _attacker.StopAttack();
    }

    private void OnAttackStarted()
    {
        _attacker.StartAttack();
    }

    private void AddItemToInventory(IItem item)
    {
        _inventoryView.Add(item);
        item.Collect();
    }
}
