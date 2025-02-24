using UnityEngine;

public class AnimatorUpdater : IAnimatorUpdater
{
    private Animator _animator;
    private IMovementHandler _movementHandler;
    private IInputHandler _inputHandler;

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
            _animator.SetFloat("Speed", speed);
            _animator.SetBool("IsAttack", _inputHandler.IsAttacking);
        }
    }
}