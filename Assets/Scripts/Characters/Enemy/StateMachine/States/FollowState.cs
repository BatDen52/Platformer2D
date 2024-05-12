using UnityEngine;

class FollowState : State
{
    private EnemyVision _vision;
    private Transform _target;
    private Mover _mover;
    private Fliper _fliper;
    private Animator _animator;

    public FollowState(StateMachine stateMachine, Animator animator, Fliper fliper, Mover mover, EnemyVision vision, float tryFindTime) : base(stateMachine)
    {
        _vision = vision;
        _mover = mover;
        _fliper = fliper;
        _animator = animator;

        Transitions = new Transition[]
        {
            new LostTargetTransition(stateMachine, vision, tryFindTime)
        };
    }

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
