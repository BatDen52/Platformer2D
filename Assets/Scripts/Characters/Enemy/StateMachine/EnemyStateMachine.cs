using System;
using System.Collections.Generic;
using UnityEngine;

class EnemyStateMachine : StateMachine
{
    public EnemyStateMachine(Fliper fliper, Mover mover, EnemyVision vision, Animator animator, EnemyAttacker attacker, WayPoint[] wayPoints,
                            float maxSqrDistance, Transform transform, float waitTime, float tryFindTime)
    {
        States = new Dictionary<Type, State>()
        {
            { typeof(PatrolState), new PatrolState(this, animator, fliper, mover, vision, wayPoints,maxSqrDistance, transform, attacker.SqrAttackDistance) },
            { typeof(IdleState), new IdleState(this, vision, waitTime, attacker.SqrAttackDistance) },
            { typeof(FollowState), new FollowState(this, animator, fliper, mover, vision, tryFindTime, attacker.SqrAttackDistance) },
            { typeof(AttackState), new AttackState(this, animator, attacker, fliper, vision, attacker.Delay) }
        };

        ChangeState<PatrolState>();
    }
}
