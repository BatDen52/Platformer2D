using System;
using UnityEngine;

public class EnemyAnimationEvent : MonoBehaviour
{
    public event Action DealingDamage;
    public event Action AttackEnded;

    public void InvkeDealingDamageEvent() => DealingDamage?.Invoke();

    public void InvkeAttackEndedEvent() => AttackEnded?.Invoke();
}
