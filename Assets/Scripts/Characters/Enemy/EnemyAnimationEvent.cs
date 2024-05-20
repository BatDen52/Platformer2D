using System;
using UnityEngine;

public class EnemyAnimationEvent : MonoBehaviour
{
    public event Action DealingDamage;
    public event Action AttackEnded;

    public void InvokeDealingDamageEvent() => DealingDamage?.Invoke();

    public void InvokeAttackEndedEvent() => AttackEnded?.Invoke();
}
