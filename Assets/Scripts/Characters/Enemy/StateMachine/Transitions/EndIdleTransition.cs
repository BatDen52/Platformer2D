class EndIdleTransition : Transition
{
    private IdleState _idleState;

    public EndIdleTransition(StateMachine stateMachine, IdleState idleState) : base(stateMachine) => _idleState = idleState;

    public override bool IsNeedTransit() => _idleState.IsEndWait;

    public override void Transit()
    {
        base.Transit();
        StateMachine.ChacgeState<PatrolState>();
    }
}
