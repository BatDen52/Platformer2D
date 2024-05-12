using UnityEngine;

class SeeTargetTransition : Transition
{
    private EnemyVision _vision;

    public SeeTargetTransition(StateMachine stateMachine, EnemyVision vision) : base(stateMachine) => _vision = vision;

    public override bool IsNeedTransit() => _vision.TrySeeTarget(out Transform _);

    public override void Transit()
    {
        base.Transit();
        StateMachine.ChacgeState<FollowState>();
    }
}
