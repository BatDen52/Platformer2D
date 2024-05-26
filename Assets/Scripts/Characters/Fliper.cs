using System;
using UnityEngine;

public class Fliper : MonoBehaviour
{
    private Transform _target;

    public bool IsTurnRight { get; private set; } = true;

    public void Initialize(Transform target)
    {
        _target = target;
    }

    public void LookAtTarget(Vector2 targetPosition)
    {
        if ((_target.position.x < targetPosition.x && IsTurnRight == false)
           || (_target.position.x > targetPosition.x && IsTurnRight))
        {
            Flip();
        }
    }

    public void Flip()
    {
        IsTurnRight = !IsTurnRight;
        _target.Flip();
    }
}
