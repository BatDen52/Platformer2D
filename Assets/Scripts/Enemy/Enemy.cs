using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private WayPoint[] _wayPoints;
    [SerializeField] private float _speedX = 1;
    [SerializeField] private float _maxSqrDistance = 0.1f;
    [SerializeField] private float _waitTime = 2f;
    [SerializeField] private Vector2 _seeAreaSize;
    [SerializeField] private LayerMask _targetLayer;

    private Rigidbody2D _rigidbody;
    private bool _isTurnRight = true;
    private int _wayPointIndex;
    private Transform _target;
    private bool _isWaiting = false;
    private float _endWaitTime;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _target = _wayPoints[_wayPointIndex].transform;
    }

    private void FixedUpdate()
    {
        if (TrySeeTarget(out Transform target))
        {
            Move(target);
            return;
        }

        if (_isWaiting == false)
            Move(_target);

        if (IsTargetReached() && _isWaiting == false)
        {
            _isWaiting = true;
            _endWaitTime = Time.time + _waitTime;
        }

        if (_isWaiting && _endWaitTime <= Time.time)
        {
            ChangeTarget();
            _isWaiting = false;
        }
    }

    private bool TrySeeTarget(out Transform target)
    {
        target = null;

        Collider2D hit = Physics2D.OverlapBox(GetLookAreaOrigin(), _seeAreaSize, 0, _targetLayer);

        if (hit != null)
        {
            Vector2 direction = (hit.transform.position - transform.position).normalized;
            RaycastHit2D hit2D = Physics2D.Raycast(transform.position, direction, _seeAreaSize.x, ~(1 << gameObject.layer));

            if (hit2D.collider != null)
            {
                if (hit2D.collider == hit)
                {
                    Debug.DrawLine(transform.position, hit2D.point, Color.red);
                    target = hit2D.transform;
                    return true;
                }
                else
                {
                    Debug.DrawLine(transform.position, hit2D.point, Color.white);
                }
            }
        }

        return false;
    }

    private void Move(Transform target)
    {
        Vector2 newPosition = Vector2.MoveTowards(transform.position, target.position, _speedX * Time.fixedDeltaTime);
        _rigidbody.MovePosition(newPosition);
    }

    private bool IsTargetReached()
    {
        float sqeDistance = (transform.position - _target.position).sqrMagnitude;

        return sqeDistance < _maxSqrDistance;
    }

    private void ChangeTarget()
    {
        _wayPointIndex = ++_wayPointIndex % _wayPoints.Length;
        _target = _wayPoints[_wayPointIndex].transform;

        if ((transform.position.x < _target.position.x && _isTurnRight == false)
            || (transform.position.x > _target.position.x && _isTurnRight))
        {
            _isTurnRight = !_isTurnRight;
            transform.Flip();
        }
    }

    private Vector2 GetLookAreaOrigin()
    {
        float halfCoefficient = 2;
        int directionCoefficient = _isTurnRight ? 1 : -1;
        float originX = transform.position.x + _seeAreaSize.x / halfCoefficient * directionCoefficient;
        return new Vector2(originX, transform.position.y);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(GetLookAreaOrigin(), _seeAreaSize);
    }
}