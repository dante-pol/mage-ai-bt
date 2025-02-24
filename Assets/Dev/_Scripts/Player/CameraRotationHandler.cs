using UnityEngine;

public class CameraRotationHandler : ICameraRotationHandler
{
    private Transform _transform;
    private float _mouseSensitivity = 2f;
    private float _rotationX = 0f;

    public CameraRotationHandler(Transform transform)
    {
        _transform = transform;
    }

    public void HandleCameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;
        float mouseY = -Input.GetAxis("Mouse Y") * _mouseSensitivity;
        _rotationX += mouseY;
        _rotationX = Mathf.Clamp(_rotationX, -90f, 90f);
        _transform.Rotate(Vector3.up * mouseX);
    }
}