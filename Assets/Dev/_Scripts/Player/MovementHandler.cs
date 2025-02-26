using UnityEngine;

public class MovementHandler : IMovementHandler
{
    private CharacterController _characterController;
    private float _moveSpeed = 5f;
    private float _jumpForce = 7f;
    private bool _isGrounded;
    private IInputHandler _inputHandler;

    public MovementHandler(CharacterController characterController, IInputHandler inputHandler)
    {
        _characterController = characterController;
        _inputHandler = inputHandler;
    }

    public void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput);
        moveDirection = _characterController.transform.TransformDirection(moveDirection);
        moveDirection *= _moveSpeed;

        HandleJump(ref moveDirection);

        _characterController.Move(moveDirection * Time.deltaTime);
    }

    private void HandleJump(ref Vector3 moveDirection)
    {
        _isGrounded = _characterController.isGrounded;
        Debug.Log(_isGrounded);

        if (_isGrounded)
        {
            moveDirection.y = 0f;

            if (_inputHandler.IsJumpPressed)
            {
                Debug.Log("Клавиша прыжка нажата!");
                moveDirection.y = _jumpForce;
            }
        }
    }

    public Vector3 GetMoveDirection()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        return new Vector3(horizontalInput, 0, verticalInput);
    }
}