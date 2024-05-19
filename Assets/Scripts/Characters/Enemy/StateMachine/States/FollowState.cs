using UnityEngine;

class FollowState : State, IMoveState
{
    private EnemyVision _vision;
    private Transform _target;
    private Mover _mover;
    private Fliper _fliper;
    private Animator _animator;

    public FollowState(StateMachine stateMachine, Animator animator, Fliper fliper, Mover mover, EnemyVision vision,
        float tryFindTime, float sqrAttackDistance) : base(stateMachine)
    {
        _vision = vision;
        _mover = mover;
        _fliper = fliper;
        _animator = animator;

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
            _mover.Run(_target);
            _fliper.LookAtTarget(_target.position);
        }
    }
}
