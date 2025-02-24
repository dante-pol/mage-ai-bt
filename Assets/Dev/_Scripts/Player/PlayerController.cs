using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private IMovementHandler _movementHandler;
    private ICameraRotationHandler _cameraRotationHandler;
    private IInputHandler _inputHandler;
    private IAnimatorUpdater _animatorUpdater;

    public void Initialize(
        IMovementHandler movementHandler,
        ICameraRotationHandler cameraRotationHandler,
        IInputHandler inputHandler,
        IAnimatorUpdater animatorUpdater)
    {
        _movementHandler = movementHandler;
        _cameraRotationHandler = cameraRotationHandler;
        _inputHandler = inputHandler;
        _animatorUpdater = animatorUpdater;
    }

    private void Update()
    {
        _movementHandler.HandleMovement();
        _cameraRotationHandler.HandleCameraRotation();
        _inputHandler.HandleInput();
        _animatorUpdater.UpdateAnimator();
    }
}