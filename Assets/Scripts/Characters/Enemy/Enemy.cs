using UnityEngine;

[RequireComponent(typeof(Attacker), typeof(EnemyVision), typeof(Mover))]
[RequireComponent(typeof(EnemySound), typeof(EnemyGroundDetector))]
public class Enemy : Character
{
    [SerializeField] private WayPoint[] _wayPoints;
    [SerializeField] private Animator _animator;
    [SerializeField] private EnemyAnimationEvent _animationEvent;
    [SerializeField] private float _maxSqrDistance = 0.1f;
    [SerializeField] private float _waitTime = 2f;
    [SerializeField] private float _tryFindTime = 1f;

    private Attacker _attacker;
    private EnemyStateMachine _stateMachine;
    private EnemyVision _vision;
    private EnemyGroundDetector _groundDetector;
    private EnemySound _audio;

    protected override void Awake()
    {
        base.Awake();

        _attacker = GetComponent<Attacker>();
        _animationEvent.DealingDamage += _attacker.Attack;
        _animationEvent.AttackEnded += _attacker.OnAttackEnded;
        _vision = GetComponent<EnemyVision>();
        _groundDetector = GetComponent<EnemyGroundDetector>();
        _audio = GetComponent<EnemySound>();
    }

    private void Start()
    {
        var mover = GetComponent<Mover>();

        _stateMachine = new EnemyStateMachine(Fliper, mover, _vision, _groundDetector, _animator, _attacker, _audio, _wayPoints, _maxSqrDistance, transform,
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
        _audio.PlayDeathSpund();
        Destroy(gameObject);
    }

    protected override void OnTakingDamage()
    {
        _animator.SetTrigger(ConstantsData.AnimatorParameters.IsHit);
        _audio.PlayHitSpund();

        if (_vision.TrySeeTarget(out _) == false)
            Fliper.Flip();
    }
}