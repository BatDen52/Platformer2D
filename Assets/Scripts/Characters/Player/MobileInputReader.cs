using UnityEngine;

public class MobileInputReader : MonoBehaviour, IInputReader
{
    [SerializeField] private VariableJoystick _joystick;
    [SerializeField] private TouchHandler _jumpButton;
    [SerializeField] private TouchHandler _attackButton;
    [SerializeField] private TouchHandler _interactButton;

    private bool _isJump;
    private bool _isInterect;
    private bool _isAttack;

    public float Direction => _joystick.Horizontal;

    private void OnEnable()
    {
        _jumpButton.Down += SetJump;
        _attackButton.Down += SetAttack;
        _interactButton.Down += SetInteract;
    }

    private void OnDisable()
    {
        _jumpButton.Down -= SetJump;
        _attackButton.Down -= SetAttack;
        _interactButton.Down -= SetInteract;
    }

    public bool GetIsJump() => GetBoolAsTrigger(ref _isJump);

    public bool GetIsInteract() => GetBoolAsTrigger(ref _isInterect);

    public bool GetIsAttack() => GetBoolAsTrigger(ref _isAttack);

    public void SetJump() => _isJump = true;

    public void SetInteract() => _isInterect = true;

    public void SetAttack() => _isAttack = true;

    private bool GetBoolAsTrigger(ref bool value)
    {
        bool lockalValue = value;
        value = false;
        return lockalValue;
    }
}
