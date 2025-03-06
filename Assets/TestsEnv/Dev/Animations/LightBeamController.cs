using UnityEngine;
using System;
using System.Collections;
using Root;

public class LightBeamController : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Transform _lightSource;
    [SerializeField] private float _beamLength = 10f;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private GameConfig _gameConfig;

    private bool _isBeamActive;
    private float _currentBeamLength;
    private float _beamDuration = 6f;
    private float _beamEndTime;

    public void ActivateBeam()
    {
        if (!_isBeamActive)
        {
            _isBeamActive = true;
            _currentBeamLength = 0f;
            _beamEndTime = Time.time + _beamDuration;
            StartCoroutine(BeamLifecycle());
        }
    }

    private IEnumerator BeamLifecycle()
    {
        float growTime = 1f;
        float startTime = Time.time;
        
        while (Time.time - startTime < growTime)
        {
            _currentBeamLength = Mathf.Lerp(0f, _beamLength, (Time.time - startTime)/growTime);
            yield return null;
        }
        _currentBeamLength = _beamLength;

        while (Time.time < _beamEndTime)
        {
            CheckForCollisions();
            yield return null;
        }

        startTime = Time.time;
        while (Time.time - startTime < growTime)
        {
            _currentBeamLength = Mathf.Lerp(_beamLength, 0f, (Time.time - startTime)/growTime);
            yield return null;
        }
        _currentBeamLength = 0f;
        EventManager.Instance.SetSuperAbilityAvailability(false);
        _isBeamActive = false;
    }

    private void CheckForCollisions()
    {
        RaycastHit[] hits = Physics.RaycastAll(_lightSource.position, _lightSource.forward, _beamLength, _enemyLayer);
        
        foreach (var hit in hits)
        {
            if (hit.collider.TryGetComponent(out IEntityAttacked entity))
            {
                entity.TakeAttack(new AttackProcess(_gameConfig.UltiDamage));
            }
            
            if (hit.collider.CompareTag("Boss"))
            {
                Debug.Log("Урон боссу!");
            }
        }
    }

    private void Update()
    {
        UpdateBeam();
    }

    private void UpdateBeam()
    {
        if (_lineRenderer != null)
        {
            _lineRenderer.enabled = _currentBeamLength > 0f;
            _lineRenderer.SetPosition(0, _lightSource.position);
            _lineRenderer.SetPosition(1, _lightSource.position + _lightSource.forward * _currentBeamLength);
        }
    }
}