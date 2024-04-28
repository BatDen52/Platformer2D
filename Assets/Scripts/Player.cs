using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(GroundDetector), typeof(PlayerMover))]
[RequireComponent(typeof(PlayerAnimator))]
public class Player : MonoBehaviour
{
    private InputReader _inputReader;  
    private GroundDetector _groundDetector;
    private PlayerMover _mover;
    private PlayerAnimator _animator;

    private void Awake()
    {
        _groundDetector = GetComponent<GroundDetector>();
        _inputReader = GetComponent<InputReader>();
        _mover = GetComponent<PlayerMover>();
        _animator = GetComponent<PlayerAnimator>();
    }

    private void FixedUpdate()
    {
        _animator.SetSpeedX(_inputReader.Direction);

        if (_inputReader.Direction != 0)
            _mover.Move(_inputReader.Direction);

        if (_inputReader.GetIsJump() && _groundDetector.IsGround)
            _mover.Jump();
    }
}
