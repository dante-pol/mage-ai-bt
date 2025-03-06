using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerHealth GetHealth() => _playerHealth;

    private IMovementHandler _movementHandler;
    private ICameraRotationHandler _cameraRotationHandler;
    private IInputHandler _inputHandler;
    private IAnimatorUpdater _animatorUpdater;
    private PlayerHealth _playerHealth;

    public void Initialize(
        IMovementHandler movementHandler,
        ICameraRotationHandler cameraRotationHandler,
        IInputHandler inputHandler,
        IAnimatorUpdater animatorUpdater,
        PlayerHealth playerHealth)
    {
        _movementHandler = movementHandler;
        _cameraRotationHandler = cameraRotationHandler;
        _inputHandler = inputHandler;
        _animatorUpdater = animatorUpdater;
        _playerHealth = playerHealth;
    }

    private void Update()
    {
        _inputHandler.HandleInput();
        _movementHandler.HandleMovement();
        _cameraRotationHandler.HandleCameraRotation();
        _animatorUpdater.UpdateAnimator();
    }
}