using Root;
using UnityEngine;

public class PlayerHealth : IEntityAttacked
{
    private float _currentHealth;
    private readonly float _maxHealth;

    public PlayerHealth(GameConfig config)
    {
        _maxHealth = config.PlayerMaxHealth;
        _currentHealth = _maxHealth;
    }

    public void TakeAttack(IAttackProcess attackProcess)
    {
        _currentHealth -= attackProcess.Damage;
        Debug.Log($"Player получил урон: {attackProcess.Damage}. Осталось: {_currentHealth}");

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player умер!");
    }

    public float GetCurrentHealth() => _currentHealth;
    public float GetMaxHealth() => _maxHealth;
}