using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(GroundDetector), typeof(Mover))]
[RequireComponent(typeof(PlayerAnimator), typeof(CollisionHandler), typeof(Fliper))]
public class Player : MonoBehaviour
{
    private InputReader _inputReader;
    private GroundDetector _groundDetector;
    private Mover _mover;
    private PlayerAnimator _animator;
    private CollisionHandler _collisionHandler;
    private Fliper _fliper;

    private IInteractable _interactable;

    private void Awake()
    {
        _groundDetector = GetComponent<GroundDetector>();
        _inputReader = GetComponent<InputReader>();
        _mover = GetComponent<Mover>();
        _animator = GetComponent<PlayerAnimator>();
        _collisionHandler = GetComponent<CollisionHandler>();
        _fliper = GetComponent<Fliper>();
    }

    private void OnEnable()
    {
        _collisionHandler.FinishReached += OnFinishReached;
    }

    private void OnDisable()
    {
        _collisionHandler.FinishReached -= OnFinishReached;
    }

    private void FixedUpdate()
    {
        _animator.SetSpeedX(_inputReader.Direction);

        if (_inputReader.Direction != 0)
        {
            _mover.Move(_inputReader.Direction);
            _fliper.LookAtTarget(transform.position + Vector3.right * _inputReader.Direction);
        }

        if (_inputReader.GetIsJump() && _groundDetector.IsGround)
            _mover.Jump();

        if (_inputReader.GetIsInteract() && _interactable != null)
            _interactable.Interact();
    }

    private void OnFinishReached(IInteractable interactable)
    {
        _interactable = interactable;
    }
}
