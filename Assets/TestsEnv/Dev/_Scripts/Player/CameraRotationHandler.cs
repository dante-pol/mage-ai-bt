using UnityEngine;

public class CameraRotationHandler : ICameraRotationHandler
{
    private Transform _playerTransform;
    private Transform _cameraPivot;
    private float _mouseSensitivity = 2f;
    private float _rotationX = 0f;

    public CameraRotationHandler(Transform playerTransform, Transform cameraPivot)
    {
        _playerTransform = playerTransform;
        _cameraPivot = cameraPivot;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void HandleCameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity;

        _rotationX -= mouseY;
        _rotationX = Mathf.Clamp(_rotationX, -90f, 90f);
        _cameraPivot.localRotation = Quaternion.Euler(_rotationX, 0f, 0f);

        _playerTransform.Rotate(Vector3.up * mouseX);
    }
}