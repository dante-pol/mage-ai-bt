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
    private bool _isShielding;
    private float _lastShieldTime = -Mathf.Infinity;
    private const float ShieldCooldown = 10f;
    private GameConfig _config;
    public event System.Action OnShieldDeactivated;
    

    public InputHandler(Animator animator, GameConfig config)
    {
        _animator = animator;
        _config = config;
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

        if (Input.GetKeyDown(KeyCode.Mouse1) && CanActivateShield())
        {
            Debug.Log("Правая нажата");
            _isShielding = true;
            EventManager.Instance.TriggerShieldActivation();
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            OnShieldDeactivated?.Invoke();
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

    private bool CanActivateShield()
    {
        return !_isShielding && (Time.time - _lastShieldTime >= _config.ShieldCooldown);
    }

    public void EndShield(bool cooldown = false)
    {
        _isShielding = false;
        if (cooldown)
        {
            _lastShieldTime = Time.time;
        }
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