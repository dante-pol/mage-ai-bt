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
    private IInputHandler _inputHandler;

    public MovementHandler(CharacterController characterController, IInputHandler inputHandler)
    {
        _characterController = characterController;
        _inputHandler = inputHandler;
        _currentSpeed = _moveSpeed;
    }

    public void HandleMovement()
    {
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
            
            if (_inputHandler.IsJumpPressed)
            {
                _verticalVelocity = _jumpForce;
                _inputHandler.ResetJump();
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
    
}