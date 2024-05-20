using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] private Sword _sword;

    public bool CanAttack => _sword.IsAttack == false && _sword.IsCooldown == false;

    public void PrepareAttack()
    {
        _sword.PrepareAttack();
    }
    
    public void StartAttack()
    {
        _sword.StartAttack();
    }

    public void StopAttack()
    {
        _sword.StopAttack();
    }
}
