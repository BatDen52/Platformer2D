using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

[RequireComponent(typeof(Fliper), typeof(EnemyVision), typeof(Mover))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private WayPoint[] _wayPoints;
    [SerializeField] private float _maxSqrDistance = 0.1f;
    [SerializeField] private float _waitTime = 2f;

    private EnemyStateMachine _stateMachine;

    private void Start()
    {
        var fliper = GetComponent<Fliper>();
        var vision = GetComponent<EnemyVision>();
        var mover = GetComponent<Mover>();

        _stateMachine = new EnemyStateMachine(fliper, mover, vision, _wayPoints, _maxSqrDistance, transform, _waitTime);
    }

    private void FixedUpdate()
    {
        _stateMachine.Update();
    }
}

abstract class StateMachine
{
    protected State CurrentState;
    protected Dictionary<Type, State> States;

    public void Update()
    {
        if (CurrentState == null)
            return;

        CurrentState.Update();
        CurrentState.TryTransit();
    }

    public void ChacgeState<TState>() where TState : State
    {
        if (CurrentState != null && CurrentState.GetType() == typeof(TState))
            return;

        if (States.TryGetValue(typeof(TState), out State newState))
        {
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
    }
}

abstract class State
{
    protected Transition[] Transitions;

    protected State(StateMachine stateMachine) { }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }

    public virtual void TryTransit()
    {
        foreach (Transition transition in Transitions)
        {
            if (transition.IsNeedTransit())
            {
                transition.Transit();
                return;
            }
        }
    }
}

abstract class Transition
{
    protected StateMachine StateMachine;

    protected Transition(StateMachine stateMachine)
    {
        StateMachine = stateMachine;
    }

    public abstract bool IsNeedTransit();

    public abstract void Transit();
}

class EnemyStateMachine : StateMachine
{
    public EnemyStateMachine(Fliper fliper, Mover mover, EnemyVision vision, WayPoint[] wayPoints, 
                            float maxSqrDistance, Transform transform, float waitTime)
    {
        States = new Dictionary<Type, State>()
        {
            {typeof(PatrolState), new PatrolState(this, fliper, mover, vision, wayPoints,maxSqrDistance, transform) },
            {typeof(IdleState), new IdleState(this, vision, waitTime) },
            {typeof(FollowState), new FollowState(this, fliper, mover, vision) }
        };

        ChacgeState<PatrolState>();
    }
}

class PatrolState : State
{
    private WayPoint[] _wayPoints;
    private Fliper _fliper;
    private Mover _mover;
    private int _wayPointIndex;
    private Transform _target;

    public PatrolState(StateMachine stateMachine, Fliper fliper, Mover mover, EnemyVision vision,
                        WayPoint[] wayPoints, float maxSqrDistance, Transform transform) : base(stateMachine)
    {
        _fliper = fliper;
        _mover = mover;
        _wayPoints = wayPoints;
        _wayPointIndex = -1;

        Transitions = new Transition[]
        {
            new SeeTargetTransition(stateMachine, vision),
            new TargetReachedTransition(stateMachine, this, maxSqrDistance, transform)
        };
    }

    public Transform Target => _target;

    public override void Enter()
    {
        ChangeTarget();
    }

    public override void Update()
    {
        _mover.Move(_target);
    }

    private void ChangeTarget()
    {
        _wayPointIndex = ++_wayPointIndex % _wayPoints.Length;
        _target = _wayPoints[_wayPointIndex].transform;

        _fliper.LookAtTarget(_target.position);
    }
}

class IdleState : State
{
    private float _endWaitTime;
    private float _waitTime;

    public IdleState(StateMachine stateMachine, EnemyVision vision, float waitTime) : base(stateMachine)
    {
        _waitTime = waitTime;

        Transitions = new Transition[]
        {
            new SeeTargetTransition(stateMachine, vision),
            new EndIdleTransition(stateMachine, this)
        };
    }

    public bool IsEndWait => _endWaitTime <= Time.time;

    public override void Enter()
    {
        _endWaitTime = Time.time + _waitTime;
    }
}

class FollowState : State
{
    private EnemyVision _vision;
    private Transform _target;
    private Mover _mover;
    private Fliper _fliper;

    public FollowState(StateMachine stateMachine, Fliper fliper, Mover mover, EnemyVision vision) : base(stateMachine)
    {
        _vision = vision;
        _mover = mover;
        _fliper = fliper;

        Transitions = new Transition[]
        {
            new LostTargetTransition(stateMachine, vision)
        };
    }

    public override void Enter()
    {
        _vision.TrySeeTarget(out _target);
    }

    public override void Update()
    {
        if (_target != null)
        {
            _mover.Move(_target);
            _fliper.LookAtTarget(_target.position);
        }
    }
}

class SeeTargetTransition : Transition
{
    private EnemyVision _vision;

    public SeeTargetTransition(StateMachine stateMachine, EnemyVision vision) : base(stateMachine) => _vision = vision;

    public override bool IsNeedTransit() => _vision.TrySeeTarget(out Transform _);

    public override void Transit() => StateMachine.ChacgeState<FollowState>();
}

class LostTargetTransition : Transition
{
    private EnemyVision _vision;

    public LostTargetTransition(StateMachine stateMachine, EnemyVision vision) : base(stateMachine) => _vision = vision;

    public override bool IsNeedTransit() => _vision.TrySeeTarget(out Transform _) == false;

    public override void Transit() => StateMachine.ChacgeState<IdleState>();
}

class EndIdleTransition : Transition
{
    private IdleState _idleState;

    public EndIdleTransition(StateMachine stateMachine, IdleState idleState) : base(stateMachine) => _idleState = idleState;

    public override bool IsNeedTransit() => _idleState.IsEndWait;

    public override void Transit() => StateMachine.ChacgeState<PatrolState>();
}

class TargetReachedTransition : Transition
{
    private PatrolState _patrolState;
    private float _maxSqrDistance = 0.1f;
    private Transform _transform;

    public TargetReachedTransition(StateMachine stateMachine, PatrolState patrolState, float maxSqrDistance, Transform transform) : base(stateMachine)
    {
        _patrolState = patrolState;
        _maxSqrDistance = maxSqrDistance;
        _transform = transform;
    }

    public override bool IsNeedTransit()
    {
        float sqeDistance = (_transform.position - _patrolState.Target.position).sqrMagnitude;

        return sqeDistance < _maxSqrDistance;
    }

    public override void Transit() => StateMachine.ChacgeState<IdleState>();
}