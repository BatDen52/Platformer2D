using System;
using System.Collections.Generic;
using UnityEngine;

class EnemyStateMachine : StateMachine
{
    public EnemyStateMachine(Fliper fliper, Mover mover, EnemyVision vision, Animator animator, WayPoint[] wayPoints,
                            float maxSqrDistance, Transform transform, float waitTime, float tryFindTime)
    {
        States = new Dictionary<Type, State>()
        {
            {typeof(PatrolState), new PatrolState(this, animator, fliper, mover, vision, wayPoints,maxSqrDistance, transform) },
            {typeof(IdleState), new IdleState(this, vision, waitTime) },
            {typeof(FollowState), new FollowState(this, animator, fliper, mover, vision, tryFindTime) }
        };

        ChacgeState<PatrolState>();
    }
}
