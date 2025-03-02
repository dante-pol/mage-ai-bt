using UnityEngine;

public class InputHandler : IInputHandler
{
    private Animator _animator;

    private bool _isAttacking;
    private bool _isJumping;
    private bool _isSprinting;
    private bool _isUlti;

    public InputHandler(Animator animator)
    {
        _animator = animator;
    }

    public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            EventManager.Instance.ToggleCamera();
        }

        if (Input.GetMouseButtonDown(0))
        {
            StartAttack();
        }

        if (_isAttacking && _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f && !_animator.IsInTransition(0))
        {
            EndAttack();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _isSprinting = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _isSprinting = false;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            _isUlti = true;
            Debug.Log("Клавиша Q нажата");
            EventManager.Instance.UseSuperAbility();
        }
    }

    private void StartAttack()
    {
        _isAttacking = true;
    }

    private void EndAttack()
    {
        _isAttacking = false;
    }

    private void Jump()
    {
        if (!_isJumping)
        {
            JumpTriggered = true;
        }
    }

    public void ResetJumpTrigger()
    {
        JumpTriggered = false;
    }

    public void ResetJump()
    {
        _isJumping = false;
    }

    public bool IsAttacking => _isAttacking;
    public bool IsJumpPressed => _isJumping;
    public bool IsSprinting => _isSprinting;
    public bool JumpTriggered { get; private set; }
    public bool IsUlti => _isUlti;
}