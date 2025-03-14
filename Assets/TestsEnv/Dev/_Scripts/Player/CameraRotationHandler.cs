using UnityEngine;

public class CameraRotationHandler : ICameraRotationHandler
{
    private Transform _playerTransform;
    private Transform _cameraPivot;
    private float _rotationX = 0f;
    private GameConfig _gameConfig;
    private bool _blockVerticalRotation = false;
    private bool _isDead = false;


    public CameraRotationHandler(Transform playerTransform, Transform cameraPivot, GameConfig gameConfig)
    {
        _playerTransform = playerTransform;
        _cameraPivot = cameraPivot;
        _gameConfig = gameConfig;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        EventManager.Instance.OnSuperAbilityUse += () => _blockVerticalRotation = true;
        EventManager.Instance.OnSuperAbilityEnd += () => _blockVerticalRotation = false;
        EventManager.Instance.OnPlayerDeath += () => _isDead = true;

    }

    public void HandleCameraRotation()
    {
        if(_isDead) return;

        float mouseX = Input.GetAxis("Mouse X") * _gameConfig.MouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * _gameConfig.MouseSensitivity;

        if (!_blockVerticalRotation)
        {
            _rotationX -= mouseY;
            _rotationX = Mathf.Clamp(_rotationX, -90f, 90f);
            _cameraPivot.localRotation = Quaternion.Euler(_rotationX, 0f, 0f);
        }

        _playerTransform.Rotate(Vector3.up * mouseX);
    }

}