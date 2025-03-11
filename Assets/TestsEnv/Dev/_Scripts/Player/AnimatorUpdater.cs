using UnityEngine;

public class AnimatorUpdater : IAnimatorUpdater
{
    private Animator _animator;
    private IMovementHandler _movementHandler;
    private IInputHandler _inputHandler;

    private float _smoothSpeed;
    private float _speedVelocity;
    private float _smoothDirection;
    private float _directionVelocity;
    private float _savedSpeed;
    private float _savedDirection;
    private const float SpeedSmoothTime = 0.3f;
    private const float DirectionSmoothTime = 0.1f;
    private bool _isDead;
    private float _idleTimer = 0f;
    private const float IdleTimeThreshold = 10f;

    public AnimatorUpdater(Animator animator, IMovementHandler movementHandler, IInputHandler inputHandler)
    {
        _animator = animator;
        _movementHandler = movementHandler;
        _inputHandler = inputHandler;

        EventManager.Instance.OnPlayerDeath += HandlePlayerDeath;
    }

    public void UpdateAnimator()
    {
        UpdateBaseLayer();
    }

    private void UpdateBaseLayer()
    {
        if (_animator == null) return;
        if (_isDead) return;

        float targetSpeed = CalculateTargetSpeed();
        _smoothSpeed = Mathf.SmoothDamp(_smoothSpeed, targetSpeed, ref _speedVelocity, SpeedSmoothTime);
        _animator.SetFloat("Speed", _smoothSpeed);


        float rawDirection = ((MovementHandler)_movementHandler).GetDirection();
        _smoothDirection = Mathf.SmoothDamp(_smoothDirection, rawDirection, ref _directionVelocity, DirectionSmoothTime);
        _animator.SetFloat("Direction", _smoothDirection);


        if (_inputHandler.IsAttacking && !_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            _animator.SetTrigger("Attack");
            _inputHandler.EndAttack();
            ResetIdleTimer();
        }


        _animator.SetBool("IsUlti", _inputHandler.IsUlti);


        bool isPlayerActive = IsPlayerActive();
        UpdateIdleState(isPlayerActive);
        Debug.Log($"{isPlayerActive}");


        HandleJumpAnimation();
    }

    private float CalculateTargetSpeed()
    {
        Vector3 moveDirection = ((MovementHandler)_movementHandler).GetMoveDirection();
        bool isSprinting = _inputHandler.IsSprinting;

        if (moveDirection.magnitude < 0.1f)
            return 0f;

        return isSprinting ? 2f : 1f;
    }

    private bool IsPlayerActive()
    {
        return _smoothSpeed > 0.1f || 
               _inputHandler.IsJumpPressed || 
               _inputHandler.IsAttacking || 
               _inputHandler.IsUlti ||
               _animator.GetCurrentAnimatorStateInfo(0).IsName("Attack");
    }
    private void HandlePlayerDeath()
    {
        _isDead = true;
        _animator.SetTrigger("Death");
        _animator.applyRootMotion = true;
    }

    private void UpdateIdleState(bool isActive)
    {
        if (isActive)
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
    }

    private void HandleJumpAnimation()
    {
        if (_inputHandler.JumpTriggered && !_animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            _animator.SetTrigger("Jump");
            _inputHandler.ResetJumpTrigger();
        }
    }

    private void ResetIdleTimer()
    {
        _idleTimer = 0f;
        _animator.SetBool("IsIdle", false);
    }
}