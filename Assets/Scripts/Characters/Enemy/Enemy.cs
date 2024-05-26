using UnityEngine;

[RequireComponent(typeof(EnemyAttacker), typeof(EnemyVision), typeof(Mover))]
public class Enemy : Character
{
    [SerializeField] private WayPoint[] _wayPoints;
    [SerializeField] private Animator _animator;
    [SerializeField] private EnemyAnimationEvent _animationEvent;
    [SerializeField] private float _maxSqrDistance = 0.1f;
    [SerializeField] private float _waitTime = 2f;
    [SerializeField] private float _tryFindTime = 1f;

    private EnemyAttacker _attacker;
    private EnemyStateMachine _stateMachine;
    private EnemyVision _vision;

    protected override void Awake()
    {
        base.Awake();

        _attacker = GetComponent<EnemyAttacker>();
        _animationEvent.DealingDamage += _attacker.Attack;
        _animationEvent.AttackEnded += _attacker.OnAttackEnded;
        _vision = GetComponent<EnemyVision>();
    }

    private void Start()
    {
        var mover = GetComponent<Mover>();

        _stateMachine = new EnemyStateMachine(Fliper, mover, _vision, _animator, _attacker, _wayPoints, _maxSqrDistance, transform,
            _waitTime, _tryFindTime);
    }

    private void FixedUpdate()
    {
        if (TimeManager.IsPaused)
            return;

        _stateMachine.Update();
    }

    private void OnDestroy()
    {
        _animationEvent.DealingDamage -= _attacker.Attack;
        _animationEvent.AttackEnded -= _attacker.OnAttackEnded;
    }

    protected override void OnDied()
    {
        Destroy(gameObject);
    }

    protected override void OnTakingDamage()
    {
        _animator.SetTrigger(ConstantsData.AnimatorParameters.IsHit);

        if (_vision.TrySeeTarget(out _) == false)
            Fliper.Flip();
    }
}