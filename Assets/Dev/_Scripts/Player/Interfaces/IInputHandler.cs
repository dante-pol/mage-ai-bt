public interface IInputHandler
{
    void HandleInput();
    bool IsAttacking { get; }
    bool IsJumpPressed { get; }
}