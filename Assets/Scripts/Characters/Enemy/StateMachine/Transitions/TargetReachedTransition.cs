using UnityEngine;

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

    public override void Transit()
    {
        base.Transit();
        StateMachine.ChacgeState<IdleState>();
    }
}