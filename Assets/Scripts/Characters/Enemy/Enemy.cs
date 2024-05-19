using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

[RequireComponent(typeof(Fliper), typeof(EnemyVision), typeof(Mover))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private WayPoint[] _wayPoints;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _maxSqrDistance = 0.1f;
    [SerializeField] private float _waitTime = 2f;
    [SerializeField] private float _tryFindTime = 1f;

    private Health _health;
    private EnemyStateMachine _stateMachine;

    private void Awake()
    {
        _health = new Health(_maxHealth);
    }

    private void Start()
    {
        var fliper = GetComponent<Fliper>();
        var vision = GetComponent<EnemyVision>();
        var mover = GetComponent<Mover>();

        _stateMachine = new EnemyStateMachine(fliper, mover, vision, _animator, _wayPoints, _maxSqrDistance, transform, _waitTime, _tryFindTime);
    }

    private void FixedUpdate()
    {
        _stateMachine.Update();
    }

    public void ApplyDamage(int damage)
    {
        _health.ApplyDamage(damage);

        if (_health.Value == 0)
            Destroy(gameObject);
    }
}