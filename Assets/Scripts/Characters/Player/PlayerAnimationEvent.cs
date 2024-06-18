using System;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    public event Action DealingDamage;
    public event Action AttackEnded;

    public void InvokeDealingDamageEvent() => DealingDamage?.Invoke();

    public void InvokeAttackEndedEvent() => AttackEnded?.Invoke();
}
