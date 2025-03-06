using UnityEngine;

public class MovementHandler : IMovementHandler
{
    private CharacterController _characterController;
    private float _currentSpeed;
    private float _verticalVelocity;
    private bool _shouldJump = false;
    private bool _isMovementLocked = false;
    private GameConfig _gameConfig;
    private IInputHandler _inputHandler;
    private bool _wasGrounded;


    public MovementHandler(CharacterController characterController, IInputHandler inputHandler, GameConfig gameConfig)
    {
        _characterController = characterController;
        _inputHandler = inputHandler;
        _gameConfig = gameConfig;
        _currentSpeed = _gameConfig.MoveSpeed;

        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        EventManager.Instance.OnMovementLock += LockMovement;
        EventManager.Instance.OnMovementUnlock += UnlockMovement;
    }

    public void LockMovement()
    {
        _isMovementLocked = true;
    }

    public void UnlockMovement()
    {
        _isMovementLocked = false;
    }

    public void HandleMovement()
    {
        if (_isMovementLocked)
        {
            return;
        }
        _currentSpeed = _inputHandler.IsSprinting ? _gameConfig.RunSpeed : _gameConfig.MoveSpeed;

        Vector3 moveDirection = GetMoveDirection();
        moveDirection = _characterController.transform.TransformDirection(moveDirection);
        moveDirection *= _currentSpeed;

        HandleJumpAndGravity(ref moveDirection);

        _characterController.Move(moveDirection * Time.deltaTime);
    }

    private void HandleJumpAndGravity(ref Vector3 moveDirection)
    {
        bool isGrounded = _characterController.isGrounded;

        if (isGrounded)
        {
            _verticalVelocity = -1f;
            
            if (_shouldJump)
            {
                _verticalVelocity = _gameConfig.JumpForce;
                _shouldJump = false;
            }
            if (!_wasGrounded)
            {
                _inputHandler.ResetJump();
            }
        }
        else
        {
            _verticalVelocity += _gameConfig.Gravity * Time.deltaTime;
        }
        _wasGrounded = isGrounded;
        moveDirection.y = _verticalVelocity;
    }

    public Vector3 GetMoveDirection()
    {
        return new Vector3(
            Input.GetAxis("Horizontal"),
            0,
            Input.GetAxis("Vertical")
        );
    }

    public float GetDirection()
    {
        Vector3 moveDirection = GetMoveDirection();
        float horizontal = moveDirection.x;
        float vertical = moveDirection.z;

        if (Mathf.Abs(horizontal) > Mathf.Abs(vertical))
        {
            return Mathf.Sign(horizontal);
        }
        else if (vertical < 0)
        {
            return -2f;
        }

        return 0f;
    }

    public void TriggerJump()
    {   
        _shouldJump = true;
    }
    
}