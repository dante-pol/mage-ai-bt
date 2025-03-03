using UnityEngine;
using System;

public class LightBeamController : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Transform _lightSource;
    [SerializeField] private float _beamLength = 10f;
    [SerializeField] private float _beamGrowSpeed = 5f;
    [SerializeField] private LayerMask _enemyLayer;

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
        if (!_isBeamActive)
        {
            _currentBeamLength = 0f;
        }
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

        if (_isBeamActive)
        {
            CheckForCollisions();
        }
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

    private void CheckForCollisions()
    {
        RaycastHit hit;
        if (Physics.Raycast(_lightSource.position, _lightSource.forward, out hit, _beamLength, _enemyLayer))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                Destroy(hit.collider.gameObject);
            }
            else if (hit.collider.CompareTag("Boss"))
            {
                Debug.Log("Нанес урон боссу");
            }
        }
    }
}