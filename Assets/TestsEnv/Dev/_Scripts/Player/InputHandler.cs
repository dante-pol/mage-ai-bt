using UnityEngine;

public class InputHandler : IInputHandler
{
    private Animator _animator;

    private bool _isAttacking;
    private bool _isJumping;
    private bool _isSprinting;
    private bool _isUlti;
    private float _lastAttackTime = -Mathf.Infinity;
    private float _attackCooldown = 0.5f;
    

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

        if (Input.GetMouseButtonDown(0) && CanAttack())
        {
            StartAttack();
            _lastAttackTime = Time.time;
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
            if(EventManager.Instance.IsSuperAbilityAvailable())
            {
                _isUlti = true;
                Debug.Log("Клавиша Q нажата");
                EventManager.Instance.TriggerSuperAbilityUse();
            }
            else
            {
                Debug.Log("Суперспособность недоступна!");
            }
        }
    }

    private bool CanAttack()
    {
        return !_isAttacking && (Time.time - _lastAttackTime >= _attackCooldown);
    }

    private void StartAttack()
    {
        _isAttacking = true;
    }

    public void EndAttack()
    {
        _isAttacking = false;
        _lastAttackTime = Time.time;
    }

    private void Jump()
    {
        if(!_isJumping)
        {
            _isJumping = true;
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
    
    public void ResetUlt()
    {
        _isUlti = false;
    }

    public bool IsAttacking => _isAttacking;
    public bool IsJumpPressed => _isJumping;
    public bool IsSprinting => _isSprinting;
    public bool JumpTriggered { get; private set; }
    public bool IsUlti => _isUlti;
}