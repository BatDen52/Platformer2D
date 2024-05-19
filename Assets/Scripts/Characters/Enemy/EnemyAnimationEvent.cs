using System;
using UnityEngine;

public class EnemyAnimationEvent : MonoBehaviour
{
    public event Action Attack;
    public event Action StartAttack;
    public event Action EndAttack;

    public void InvkeAttackEvent() => Attack?.Invoke();

    public void InvkeStartAttackEvent() => StartAttack?.Invoke();

    public void InvkeEndAttackEvent() => EndAttack?.Invoke();
}
