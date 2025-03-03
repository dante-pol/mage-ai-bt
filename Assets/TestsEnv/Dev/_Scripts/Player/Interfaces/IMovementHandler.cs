using UnityEngine;

public interface IMovementHandler
{
    void HandleMovement();
    Vector3 GetMoveDirection();
}