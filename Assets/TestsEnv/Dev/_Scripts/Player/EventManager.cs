using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    public event Action OnToggleCamera;
    public event Action<Vector3, Vector3> OnAttack;
    public event Action OnSuperAbilityUse;
    public event Action OnEnemyHit;


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

    public void ToggleCamera()
    {
        OnToggleCamera?.Invoke();
    }

    public void TriggerAttack(Vector3 position, Vector3 direction)
    {
        OnAttack?.Invoke(position, direction);
    }

    public void UseSuperAbility()
    {
        Debug.Log("Сфывфывфывфыв");
        OnSuperAbilityUse?.Invoke();
    }

    public void TriggerHitEnemy()
    {
        OnEnemyHit?.Invoke();
    }

}