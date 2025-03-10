using UnityEngine;
using Root;

public class PlayerHealth : MonoBehaviour, IEntityAttacked 
{
    public Teams TeamID => _teamID;
    
    [SerializeField] private Teams _teamID;
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

    private void OnGUI()
    {
        var labelStyle = new GUIStyle(GUI.skin.label);
        labelStyle.fontSize = 48; // Укажи нужный размер шрифта
        labelStyle.normal.textColor = Color.white; // Цвет текста

        GUI.Label(new Rect(25,25,350, 200), _currentHealth.ToString(), labelStyle);
    }
}