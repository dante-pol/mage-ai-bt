using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    public event Action OnToggleCamera;
    public event Action<Vector3, Vector3> OnAttack;
    public event Action OnSuperAbilityUse;
    public event Action OnSuperAbilityEnd;
    public event Action OnMovementLock;
    public event Action OnMovementUnlock;
    public event Action OnEnemyHit;
    public event Action OnShieldActivation;
    public event Action OnPlayerDeath;
    public event Action OnInputLock;
    private bool _isSuperAbilityAvailable = false;


    public void TriggerPlayerDeath()
    {
        OnPlayerDeath?.Invoke();
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool IsSuperAbilityAvailable()
    {
        return _isSuperAbilityAvailable;
    }

    public void SetSuperAbilityAvailability(bool isAvailable)
    {
        _isSuperAbilityAvailable = isAvailable;
    }

    public void ToggleCamera()
    {
        OnToggleCamera?.Invoke();
    }

    public void TriggerAttack(Vector3 position, Vector3 direction)
    {
        OnAttack?.Invoke(position, direction);
    }

    public void TriggerSuperAbilityUse()
    {
        Debug.Log("Сфывфывфывфыв");
        OnSuperAbilityUse?.Invoke();
    }

    public void TriggerSuperAbilityEnd()
    {
        OnSuperAbilityEnd?.Invoke();
    }

    public void TriggerMovementLock()
    {
        OnMovementLock?.Invoke();
    }

    public void TriggerMovementUnlock()
    {
        OnMovementUnlock?.Invoke();
    }


    public void TriggerHitEnemy()
    {
        OnEnemyHit?.Invoke();
    }

    public void TriggerShieldActivation()
    {
        OnShieldActivation?.Invoke();
    }

    public void TriggerInputLock()
    {
        OnInputLock?.Invoke();
    }
}