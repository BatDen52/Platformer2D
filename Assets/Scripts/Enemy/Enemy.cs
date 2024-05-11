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
        Collider2D hit = Physics2D.OverlapBox(GetLookAreaOrigin(), _seeAreaSize, 0, _targetLayer);

        if (hit != null)
        {
            Debug.Log(hit.gameObject.name);
        }

        if (_isWaiting == false)
            Move();

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

    private void Move()
    {
        Vector2 newPosition = Vector2.MoveTowards(transform.position, _target.position, _speedX * Time.fixedDeltaTime);
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