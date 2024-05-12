using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

[RequireComponent(typeof(Fliper), typeof(EnemyVision), typeof(Mover))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private WayPoint[] _wayPoints;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _maxSqrDistance = 0.1f;
    [SerializeField] private float _waitTime = 2f;
    [SerializeField] private float _tryFindTime = 1f;

    private EnemyStateMachine _stateMachine;

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
}