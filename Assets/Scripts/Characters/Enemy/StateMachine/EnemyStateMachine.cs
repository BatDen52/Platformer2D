using System;
using System.Collections.Generic;
using UnityEngine;

class EnemyStateMachine : StateMachine
{
    public EnemyStateMachine(Fliper fliper, Mover mover, EnemyVision vision, EnemyGroundDetector groundDetector, Animator animator, Attacker attacker, EnemySound audio, WayPoint[] wayPoints,
                            float maxSqrDistance, Transform transform, float waitTime, float tryFindTime)
    {
        States = new Dictionary<Type, State>()
        {
            { typeof(PatrolState), new PatrolState(this, animator, fliper, mover, vision, audio, wayPoints,maxSqrDistance, transform, attacker.SqrAttackDistance) },
            { typeof(IdleState), new IdleState(this, vision, waitTime, attacker.SqrAttackDistance) },
            { typeof(FollowState), new FollowState(this, animator, fliper, mover, vision, groundDetector, audio, tryFindTime, attacker.SqrAttackDistance) },
            { typeof(AttackState), new AttackState(this, animator, attacker, fliper, vision, audio, attacker.Delay) }
        };

        ChangeState<PatrolState>();
    }
}
