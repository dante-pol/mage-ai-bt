using UnityEngine;
using Cinemachine;
using System;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _firstPersonCamera;
    [SerializeField] private CinemachineVirtualCamera _thirdPersonCamera;

    private CinemachineVirtualCamera activeCamera;
    private Action _cameraToggleCallback;

    public void ToggleCamera()
    {
        if (activeCamera == _firstPersonCamera)
        {
            SwitchToThirdPersonCamera();
        }
        else
        {
            SwitchToFirstPersonCamera();
        }
    }

    public void SubscribeToCameraToggleEvent(Action toggleCallback)
    {
        _cameraToggleCallback = toggleCallback;
        if (_cameraToggleCallback != null)
        {
            EventManager.Instance.OnToggleCamera += _cameraToggleCallback;
        }
    }

    private void OnDisable()
    {
        if (_cameraToggleCallback != null)
        {
            EventManager.Instance.OnToggleCamera -= _cameraToggleCallback;
        }
    }

    private void SwitchToFirstPersonCamera()
    {
        _firstPersonCamera.Priority = 10;
        _thirdPersonCamera.Priority = 0;
        activeCamera = _firstPersonCamera;
    }

    private void SwitchToThirdPersonCamera()
    {
        _firstPersonCamera.Priority = 0;
        _thirdPersonCamera.Priority = 10;
        activeCamera = _thirdPersonCamera;
    }

    private void Start()
    {
        SubscribeToCameraToggleEvent(ToggleCamera);
        SwitchToThirdPersonCamera();
    }
}