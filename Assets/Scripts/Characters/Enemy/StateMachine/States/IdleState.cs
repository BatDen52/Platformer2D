using UnityEngine;

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
