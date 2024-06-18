using System;
using UnityEngine;

[RequireComponent(typeof(Attacker), typeof(PlayerSound), typeof(Mover))]
[RequireComponent(typeof(PlayerAnimator), typeof(CollisionHandler))]
public class Player : Character
{
    [SerializeField] private PlayerAnimationEvent _animationEvent;
    [SerializeField] private Canvas _interactableCanvas;
    [SerializeField] private InventoryView _inventoryView;
    [SerializeField] private GroundDetector _groundDetector;

    private IInputReader _inputReader;
    private Mover _mover;
    private PlayerAnimator _animator;
    private Attacker _attacker;
    private CollisionHandler _collisionHandler;
    private PlayerSound _audio;

    private Inventory _inventory;

    private IInteractable _interactable;

    protected override void Awake()
    {
        base.Awake();
        _mover = GetComponent<Mover>();
        _animator = GetComponent<PlayerAnimator>();
        _attacker = GetComponent<Attacker>();
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
        _animationEvent.DealingDamage += _attacker.Attack;
        _animationEvent.AttackEnded += _attacker.OnAttackEnded;

        _inventory.ItemAdded += AddItemToInventory;
        _inventory.ItemRemoved += _inventoryView.Remove;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        _collisionHandler.InteractableFounded -= OnInteractableFounded;
        _collisionHandler.MedKitFounded -= OnMedKitFounded;
        _collisionHandler.KeyFounded -= OnKeyFounded;
        _animationEvent.DealingDamage -= _attacker.Attack;
        _animationEvent.AttackEnded -= _attacker.OnAttackEnded;

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
            Fliper.LookAtTarget(View.transform.position + Vector3.right * _inputReader.Direction);

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
            _attacker.StartAttack();
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
                else
                {
                    _interactable.Interact();
                }
            }
            else
            {
                _interactable.Interact();
            }
        }
    }

    public void Initialize(IInputReader inputReader) => _inputReader = inputReader;

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
        if (Health.Value < Health.MaxValue)
        {
            Heal(medKit.Value);
            medKit.Collect();
        }
    }

    private void OnKeyFounded(Key key)
    {
        _inventory.Add(key);
    }

    private void AddItemToInventory(IItem item)
    {
        _inventoryView.Add(item);
        item.Collect();
    }
}
