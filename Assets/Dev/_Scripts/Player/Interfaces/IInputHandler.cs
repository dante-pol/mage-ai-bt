public interface IInputHandler
{
    void HandleInput();
    void ResetJump();
    bool IsAttacking { get; }
    bool IsJumpPressed { get; }

    bool IsSprinting { get; }
}