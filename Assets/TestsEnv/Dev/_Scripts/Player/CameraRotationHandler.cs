using UnityEngine;

public class CameraRotationHandler : ICameraRotationHandler
{
    private Transform _playerTransform;
    private Transform _cameraPivot;
    private float _rotationX = 0f;
    private GameConfig _gameConfig;

    public CameraRotationHandler(Transform playerTransform, Transform cameraPivot, GameConfig gameConfig)
    {
        _playerTransform = playerTransform;
        _cameraPivot = cameraPivot;
        _gameConfig = gameConfig;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void HandleCameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * _gameConfig.MouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * _gameConfig.MouseSensitivity;

        _rotationX -= mouseY;
        _rotationX = Mathf.Clamp(_rotationX, -90f, 90f);
        _cameraPivot.localRotation = Quaternion.Euler(_rotationX, 0f, 0f);

        _playerTransform.Rotate(Vector3.up * mouseX);
    }
}