using UnityEngine;

public class MovementHandler : IMovementHandler
{
    private CharacterController _characterController;
    private float _moveSpeed = 5f;
    private float _runSpeed = 10f;
    private float _currentSpeed;
    private float _jumpForce = 6f;
    private float _gravity = -15f;
    private float _verticalVelocity;
    private bool _shouldJump = false;
    private bool _isMovementLocked = false;
    private IInputHandler _inputHandler;

    public MovementHandler(CharacterController characterController, IInputHandler inputHandler)
    {
        _characterController = characterController;
        _inputHandler = inputHandler;
        _currentSpeed = _moveSpeed;
        
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
        _currentSpeed = _inputHandler.IsSprinting ? _runSpeed : _moveSpeed;

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
                _verticalVelocity = _jumpForce;
                _inputHandler.ResetJump();
                _shouldJump = false;
            }
        }
        else
        {
            _verticalVelocity += _gravity * Time.deltaTime;
        }

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