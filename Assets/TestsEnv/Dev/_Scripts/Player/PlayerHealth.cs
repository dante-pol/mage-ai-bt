using UnityEngine;
using Root;

public class PlayerHealth : MonoBehaviour, IEntityAttacked 
{
    [SerializeField] private GameConfig _config;
    private float _currentHealth;

    private void Awake()
    {
        _currentHealth = _config.PlayerMaxHealth;
    }

    public void TakeAttack(IAttackProcess attackProcess)
    {
        _currentHealth -= attackProcess.Damage;
        Debug.Log($"Урон: {attackProcess.Damage}. Осталось: {_currentHealth}");

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Игрок умер!");
    }
}