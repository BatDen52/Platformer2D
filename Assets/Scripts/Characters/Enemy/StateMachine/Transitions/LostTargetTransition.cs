using UnityEngine;

class LostTargetTransition : Transition
{
    private EnemyVision _vision;
    private float _endFindTime;
    private float _tryFindTime = 1;

    public LostTargetTransition(StateMachine stateMachine, EnemyVision vision, float tryFindTime) : base(stateMachine)
    {
        _tryFindTime = tryFindTime;
        _vision = vision;
    }

    public override bool IsNeedTransit()
    {
        if (_vision.TrySeeTarget(out Transform _))
        {
            _endFindTime = Time.time + _tryFindTime;
        }
        else if (_endFindTime < Time.time)
        {
            return true;
        }

        return false;
    }
    public override void Transit()
    {
        base.Transit();
        StateMachine.ChangeState<IdleState>();
    }
}
