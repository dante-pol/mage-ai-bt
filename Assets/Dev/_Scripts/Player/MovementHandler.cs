using UnityEngine;

public class MovementHandler : IMovementHandler
{
    private CharacterController _characterController;
    private float _moveSpeed = 5f;

    public MovementHandler(CharacterController characterController)
    {
        _characterController = characterController;
    }

    public void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput);
        moveDirection = _characterController.transform.TransformDirection(moveDirection);
        moveDirection *= _moveSpeed;
        _characterController.Move(moveDirection * Time.deltaTime);
    }

    public Vector3 GetMoveDirection()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        return new Vector3(horizontalInput, 0, verticalInput);
    }
}