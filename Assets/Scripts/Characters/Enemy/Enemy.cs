using UnityEngine;

[RequireComponent(typeof(Fliper), typeof(EnemyVision), typeof(Mover))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private WayPoint[] _wayPoints;
    [SerializeField] private float _maxSqrDistance = 0.1f;
    [SerializeField] private float _waitTime = 2f;

    private Fliper _fliper;
    private EnemyVision _vision;
    private Mover _mover;
    private int _wayPointIndex;
    private Transform _target;
    private bool _isWaiting = false;
    private float _endWaitTime;

    private void Start()
    {
        _fliper = GetComponent<Fliper>();
        _vision = GetComponent<EnemyVision>();
        _mover = GetComponent<Mover>();
        _target = _wayPoints[_wayPointIndex].transform;
    }

    private void FixedUpdate()
    {
        if (_vision.TrySeeTarget(out Transform target))
        {
            _mover.Move(target);
            return;
        }

        if (_isWaiting == false)
            _mover.Move(_target);

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

    private bool IsTargetReached()
    {
        float sqeDistance = (transform.position - _target.position).sqrMagnitude;

        return sqeDistance < _maxSqrDistance;
    }

    private void ChangeTarget()
    {
        _wayPointIndex = ++_wayPointIndex % _wayPoints.Length;
        _target = _wayPoints[_wayPointIndex].transform;

        _fliper.LookAtTarget(_target.position);
    }
}