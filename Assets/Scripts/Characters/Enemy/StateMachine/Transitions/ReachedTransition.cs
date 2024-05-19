using UnityEngine;

class ReachedTransition : Transition
{
    private IMoveState _moveState;
    private float _maxSqrDistance = 0.1f;
    private Transform _transform;

    public ReachedTransition(StateMachine stateMachine, IMoveState moveState, float maxSqrDistance, Transform transform) : base(stateMachine)
    {
        _moveState = moveState;
        _maxSqrDistance = maxSqrDistance;
        _transform = transform;
    }

    public override bool IsNeedTransit()
    {
        float sqeDistance = (_transform.position - _moveState.Target.position).sqrMagnitude;

        return sqeDistance < _maxSqrDistance;
    }
}

