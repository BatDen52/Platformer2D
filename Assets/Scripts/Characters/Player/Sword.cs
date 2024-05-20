using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _delay = 2;

    private float _endWaitTime;

    public bool IsAttack { get; private set; }

    public bool IsCooldown => _endWaitTime > Time.time;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsAttack && collision.TryGetComponent(out Enemy enemy))
            enemy.ApplyDamage(_damage);
    }

    public void StartAttack()
    {
        IsAttack = true;
    }

    public void StopAttack()
    {
        IsAttack = false;
    }

    public void PrepareAttack()
    {
        _endWaitTime = Time.time + _delay;
    }
}
