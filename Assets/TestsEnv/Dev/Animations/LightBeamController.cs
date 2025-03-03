using UnityEngine;
using System;

public class LightBeamController : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Transform _lightSource;
    [SerializeField] private float _beamLength = 10f;
    [SerializeField] private float _beamGrowSpeed = 5f;

    private bool _isBeamActive;
    private float _currentBeamLength;
    private Action _toggleBeamCallback;

    private void Start()
    {
        SubscribeToSuperAbilityEvent(ToggleBeam);
    }

    private void SubscribeToSuperAbilityEvent(Action toggleCallback)
    {
        _toggleBeamCallback = toggleCallback;
        if (_toggleBeamCallback != null && EventManager.Instance != null)
        {
            EventManager.Instance.OnSuperAbilityUse += _toggleBeamCallback;
        }
    }

    private void OnDisable()
    {
        if (_toggleBeamCallback != null && EventManager.Instance != null)
        {
            EventManager.Instance.OnSuperAbilityUse -= _toggleBeamCallback;
        }
    }

    private void ToggleBeam()
    {
        Debug.Log("activate beam");
        _isBeamActive = !_isBeamActive;
    }

    private void Update()
    {
        if (_isBeamActive)
        {
            _currentBeamLength += _beamGrowSpeed * Time.deltaTime;
            _currentBeamLength = Mathf.Min(_currentBeamLength, _beamLength);
        }
        else
        {
            _currentBeamLength -= _beamGrowSpeed * Time.deltaTime;
            _currentBeamLength = Mathf.Max(_currentBeamLength, 0f);
        }

        _lineRenderer.enabled = _currentBeamLength > 0f;
        UpdateBeam();
    }

    private void UpdateBeam()
    {
        if (_lineRenderer == null || _lightSource == null)
        {
            Debug.LogError("_lineRenderer or _lightSource is not assigned!");
            return;
        }

        _lineRenderer.SetPosition(0, _lightSource.position);
        _lineRenderer.SetPosition(1, _lightSource.position + _lightSource.forward * _currentBeamLength);
    }
}