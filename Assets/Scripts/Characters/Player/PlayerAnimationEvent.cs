using System;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    public event Action AttackStarted;
    public event Action AttackEnded;

    public void InvkeAttackStartedEvent() => AttackStarted?.Invoke();

    public void InvkeAttackEndedEvent() => AttackEnded?.Invoke();
}
