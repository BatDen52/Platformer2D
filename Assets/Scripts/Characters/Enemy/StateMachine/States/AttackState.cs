using UnityEngine;

class AttackState : State
{
    private EnemyAttacker _attacker;
    private Animator _animator;
    private EnemyVision _vision;
    private Fliper _fliper;
    private EnemySound _audio;
    private LostTargetTransition _lostTargetTransition;
    private Transform _target;

    public AttackState(StateMachine stateMachine, Animator animator, EnemyAttacker attacker,
                        Fliper fliper, EnemyVision vision, EnemySound audio, float tryFindTime) : base(stateMachine)
    {
        _animator = animator;
        _attacker = attacker;
        _vision = vision;
        _fliper = fliper;
        _audio = audio;

        _lostTargetTransition = new LostTargetTransition(stateMachine, vision, tryFindTime);

        Transitions = new Transition[]
        {
            new SeeTargetTransition(stateMachine, vision, vision.transform, _attacker.SqrAttackDistance),
            _lostTargetTransition
        };
    }

    public override void Enter()
    {
        _vision.TrySeeTarget(out _target);
        _lostTargetTransition.IsNeedTransit();
    }

    public override void Update()
    {
        if (_attacker.IsAttack == false)
            _fliper.LookAtTarget(_target.position);

        if (_attacker.CanAttack)
        {
            _attacker.StartAttack();
            _animator.SetTrigger(ConstantsData.AnimatorParameters.IsAttack);
            _audio.PlayAttackSpund();
        }
    }

    public override void TryTransit()
    {
        if (_attacker.IsAttack == false)
            base.TryTransit();
    }
}
