using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditorInternal;
using UnityEngine;

[RequireComponent(typeof(Fliper), typeof(EnemyVision), typeof(Mover))]
[RequireComponent(typeof(EnemyAttacker))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private WayPoint[] _wayPoints;
    [SerializeField] private Animator _animator;
    [SerializeField] private EnemyAnimationEvent _animationEvent;
    [SerializeField] private float _maxSqrDistance = 0.1f;
    [SerializeField] private float _waitTime = 2f;
    [SerializeField] private float _tryFindTime = 1f;

    private Health _health;
    private EnemyAttacker _attacker;
    private EnemyStateMachine _stateMachine;

    private void Awake()
    {
        _health = new Health(_maxHealth);
        _attacker = GetComponent<EnemyAttacker>();
        _animationEvent.DealingDamage += _attacker.Attack;
        _animationEvent.AttackEnded += _attacker.OnAttackEnded;
        _health.TakingDamage += OnTakingDamage;
    }

    private void Start()
    {
        var fliper = GetComponent<Fliper>();
        var vision = GetComponent<EnemyVision>();
        var mover = GetComponent<Mover>();

        _stateMachine = new EnemyStateMachine(fliper, mover, vision, _animator, _attacker, _wayPoints, _maxSqrDistance, transform,
            _waitTime, _tryFindTime);
    }

    private void FixedUpdate()
    {
        _stateMachine.Update();
    }

    private void OnDestroy()
    {
        _animationEvent.DealingDamage -= _attacker.Attack;
        _animationEvent.AttackEnded -= _attacker.OnAttackEnded;
        _health.TakingDamage -= OnTakingDamage;
    }

    public void ApplyDamage(int damage)
    {
        _health.ApplyDamage(damage);

        if (_health.Value == 0)
            Destroy(gameObject);
    }

    private void OnTakingDamage()
    {
        _animator.SetTrigger(ConstantsData.AnimatorParameters.IsHit);
    }
}