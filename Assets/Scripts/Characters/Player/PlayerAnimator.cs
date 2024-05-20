using System;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void SetSpeedX(float speedX)
    {
        _animator.SetFloat(ConstantsData.AnimatorParameters.SpeedX, Mathf.Abs(speedX));
    }

    public void SetAttackTrigger()
    {
        _animator.SetTrigger(ConstantsData.AnimatorParameters.IsAttack);
    }

    public void SetHitTrigger()
    {
        _animator.SetTrigger(ConstantsData.AnimatorParameters.IsHit);
    }
}
