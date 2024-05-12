using UnityEngine;

class PatrolState : State
{
    private WayPoint[] _wayPoints;
    private Animator _animator;
    private Fliper _fliper;
    private Mover _mover;
    private int _wayPointIndex;
    private Transform _target;

    public PatrolState(StateMachine stateMachine, Animator animator, Fliper fliper, Mover mover, EnemyVision vision,
                        WayPoint[] wayPoints, float maxSqrDistance, Transform transform) : base(stateMachine)
    {
        _animator = animator;
        _fliper = fliper;
        _mover = mover;
        _wayPoints = wayPoints;
        _wayPointIndex = -1;

        var targetReachedTransition = new TargetReachedTransition(stateMachine, this, maxSqrDistance, transform);
        targetReachedTransition.Transiting += ChangeTarget;

        Transitions = new Transition[]
        {
            new SeeTargetTransition(stateMachine, vision),
            targetReachedTransition
        };

        ChangeTarget();
    }

    public Transform Target => _target;

    public override void Enter()
    {
        _fliper.LookAtTarget(_target.position);
        _animator.SetBool(ConstantsData.AnimatorParameters.IsWalk, true);
    }

    public override void Exit()
    {
        _animator.SetBool(ConstantsData.AnimatorParameters.IsWalk, false);
    }

    public override void Update()
    {
        _mover.Walk(_target);
    }

    private void ChangeTarget()
    {
        _wayPointIndex = ++_wayPointIndex % _wayPoints.Length;
        _target = _wayPoints[_wayPointIndex].transform;
    }
}
