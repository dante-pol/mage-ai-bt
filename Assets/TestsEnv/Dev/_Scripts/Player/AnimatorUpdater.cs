using UnityEngine;

public class AnimatorUpdater : IAnimatorUpdater
{
    private Animator _animator;
    private IMovementHandler _movementHandler;
    private IInputHandler _inputHandler;

    private float _idleTimer = 0f;
    private const float IdleTimeThreshold = 10f;

    public AnimatorUpdater(Animator animator, IMovementHandler movementHandler, IInputHandler inputHandler)
    {
        _animator = animator;
        _movementHandler = movementHandler;
        _inputHandler = inputHandler;
    }

    public void UpdateAnimator()
    {
        if (_animator != null)
        {
            float speed = ((MovementHandler)_movementHandler).GetMoveDirection().magnitude;

            float direction = ((MovementHandler)_movementHandler).GetDirection();

            _animator.SetFloat("Speed", speed);

            _animator.SetFloat("Direction", direction);

            if(_inputHandler.IsAttacking)
            {
                _animator.SetTrigger("Attack");  
                _inputHandler.EndAttack();
            }
        
            _animator.SetBool("IsRun", _inputHandler.IsSprinting);
            _animator.SetBool("IsUlti", _inputHandler.IsUlti);

            bool isPlayerActive = CheckPlayerActivity(speed);

            if (isPlayerActive)
            {
                _idleTimer = 0f;
                _animator.SetBool("IsIdle", false);
            }
            else
            {
                _idleTimer += Time.deltaTime;

                if (_idleTimer >= IdleTimeThreshold)
                {
                    _animator.SetBool("IsIdle", true);
                }
            }

            if (_inputHandler.JumpTriggered)
            {
                _animator.SetTrigger("Jump");
                _inputHandler.ResetJumpTrigger();
            }
        }
    }

    private bool CheckPlayerActivity(float speed)
    {
        return speed > 0 || _inputHandler.IsJumpPressed || _inputHandler.IsAttacking || _inputHandler.IsUlti;
    }
}