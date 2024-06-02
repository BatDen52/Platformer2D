using UnityEngine;

class FollowState : State, IMoveState
{
    private EnemyVision _vision;
    private EnemyGroundDetector _groundDetector;
    private Transform _target;
    private Mover _mover;
    private Fliper _fliper;
    private Animator _animator;
    private EnemySound _audio;
    private bool _isRun;

    public FollowState(StateMachine stateMachine, Animator animator, Fliper fliper, Mover mover, EnemyVision vision, EnemyGroundDetector groundDetector,
        EnemySound audio, float tryFindTime, float sqrAttackDistance) : base(stateMachine)
    {
        _vision = vision;
        _groundDetector = groundDetector;
        _mover = mover;
        _fliper = fliper;
        _animator = animator;
        _audio = audio;

        Transitions = new Transition[]
        {
            new LostTargetTransition(stateMachine, vision, tryFindTime),
            new TargetReachedTransition(stateMachine, this, sqrAttackDistance, _mover.transform)
        };
    }

    public Transform Target => _target;

    public override void Enter()
    {
        _vision.TrySeeTarget(out _target);

        if (_groundDetector.CanMove())
            _animator.SetBool(ConstantsData.AnimatorParameters.IsRun, true);
    }

    public override void Exit()
    {
        _animator.SetBool(ConstantsData.AnimatorParameters.IsRun, false);
    }

    public override void Update()
    {
        if (_target != null)
        {
            if (_groundDetector.CanMove())
            {
                if (_isRun == false)
                {
                    _animator.SetBool(ConstantsData.AnimatorParameters.IsRun, true);
                    _isRun = true;
                }

                _mover.Run(_target);
                _audio.PlayRunSpund();
                _fliper.LookAtTarget(_target.position);
            }
            else if (_isRun)
            {
                _animator.SetBool(ConstantsData.AnimatorParameters.IsRun, false);
                _isRun = false;
            }
        }
    }
}
