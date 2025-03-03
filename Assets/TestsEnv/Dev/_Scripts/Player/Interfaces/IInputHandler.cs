public interface IInputHandler
{
    void HandleInput();
    void ResetJump();
    bool IsAttacking { get; }
    bool IsJumpPressed { get; }
    bool IsSprinting { get; }
    bool IsUlti { get; }
    bool JumpTriggered { get; }
    void ResetJumpTrigger();
    void EndAttack();
    void ResetUlt();
}