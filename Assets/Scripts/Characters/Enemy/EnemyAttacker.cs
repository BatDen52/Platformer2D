using UnityEngine;

[RequireComponent(typeof(Fliper))]
public class EnemyAttacker : MonoBehaviour
{
    [SerializeField] private float _delay = 4;
    [SerializeField] private int _damage;
    [SerializeField] private float _radius;
    [SerializeField] private float _offsetX;
    [SerializeField] private LayerMask _targetLayer;

    private Fliper _fliper;
    private float _endWaitTime;

    public float Delay => _delay;
    public float SqrAttackDistance => _offsetX * _offsetX;

    public bool CanAttack => _endWaitTime <= Time.time;

    public bool IsAttack { get; private set; }

    private void Start()
    {
        _fliper = GetComponent<Fliper>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(GetAttackOrigin(), _radius);
    }

    public void Attack()
    {
        Collider2D hit = Physics2D.OverlapCircle(GetAttackOrigin(), _radius, _targetLayer);

        if (hit != null && hit.TryGetComponent(out Player player))
        {
            player.ApplyDamage(_damage);
        }
    }


    public void StartAttack()
    {
        IsAttack = true;
        _endWaitTime = Time.time + _delay;
    }

    public void OnAttackEnded() => IsAttack = false;
    
    private Vector2 GetAttackOrigin()
    {
        int directionCoefficient = _fliper?.IsTurnRight ?? true ? 1 : -1;
        float originX = transform.position.x + _offsetX * directionCoefficient;
        return new Vector2(originX, transform.position.y);
    }
}
