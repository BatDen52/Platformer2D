using UnityEngine;

public class PlayerAttackStateBehaviour : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.transform.root.TryGetComponent(out PlayerAttacker attacker))
            attacker.StopAttack();
    }
}