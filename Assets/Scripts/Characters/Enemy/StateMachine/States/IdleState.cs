using UnityEngine;

class IdleState : State
{
    private float _endWaitTime;
    private float _waitTime;

    public IdleState(StateMachine stateMachine, EnemyVision vision, float waitTime, float sqrAttackDistance) : base(stateMachine)
    {
        _waitTime = waitTime;

        Transitions = new Transition[]
        {
            new SeeTargetTransition(stateMachine, vision, vision.transform, sqrAttackDistance),
            new EndIdleTransition(stateMachine, this)
        };
    }

    public bool IsEndWait => _endWaitTime <= Time.time;

    public override void Enter()
    {
        _endWaitTime = Time.time + _waitTime;
    }
}
