using UnityEngine;

class AttackState : State
{
    private EnemyAttacker _attacker;
    private Animator _animator;
    private EnemyVision _vision;
    private Fliper _fliper;
    private Transform _target;

    public AttackState(StateMachine stateMachine, Animator animator, EnemyAttacker attacker,
                        Fliper fliper, EnemyVision vision, float tryFindTime) : base(stateMachine)
    {
        _animator = animator;
        _attacker = attacker;
        _vision = vision;
        _fliper = fliper;

        Transitions = new Transition[]
        {
            new SeeTargetTransition(stateMachine, vision, vision.transform, _attacker.SqrAttackDistance),
            new LostTargetTransition(stateMachine, vision, tryFindTime)
        };
    }

    public override void Enter()
    {
        _vision.TrySeeTarget(out _target);
    }

    public override void Update()
    {
        if (_attacker.IsAttack == false)
        {
            Debug.Log("LookAtTarget");
            _fliper.LookAtTarget(_target.position);
        }

        if (_attacker.CanAttack)
        {
            _attacker.Attack();
            _animator.SetTrigger(ConstantsData.AnimatorParameters.IsAttack);
        }
    }

    public override void TryTransit()
    {
        if (_attacker.IsAttack == false)
            base.TryTransit();
    }
}
