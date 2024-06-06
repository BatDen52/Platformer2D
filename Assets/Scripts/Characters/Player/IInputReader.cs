public interface IInputReader
{
    public float Direction { get; }
    public bool GetIsJump();
    public bool GetIsInteract();
    public bool GetIsAttack();

}
