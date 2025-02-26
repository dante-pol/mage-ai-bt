using UnityEngine;

public class InputHandler : IInputHandler
{
    private Animator _animator;
    private bool _isAttacking;
    private bool _isJumping;

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
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void StartAttack()
    {
        _isAttacking = true;
        EventManager.Instance.TriggerAttack(_animator.transform.position, _animator.transform.forward);
    }

    private void EndAttack()
    {
        _isAttacking = false;
    }

    private void Jump()
    {
        if (!_isJumping)
        {
            _isJumping = true;
        }
    }

    public void ResetJump()
    {
        _isJumping = false;
    }

    public bool IsAttacking => _isAttacking;
    public bool IsJumpPressed => _isJumping;

}