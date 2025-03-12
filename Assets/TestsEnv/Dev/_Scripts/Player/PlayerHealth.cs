using UnityEngine;
using Root;

public class PlayerHealth : MonoBehaviour, IEntityAttacked, IEntityInfo
{
    public Teams TeamID => _teamID;
    public float CurrentHealth => _currentHealth;
    public Vector3 Position => transform.position;
    public bool IsUltUse => _inputHandler.IsUlti;
    
    [SerializeField] private Teams _teamID;
    private GameConfig _config;
    private InputHandler _inputHandler;
    private float _currentHealth;
    private AudioSource _audioSource;




    public void Initialize(GameConfig gameConfig, InputHandler inputHandler)
    {
        _config = gameConfig;
        _inputHandler = inputHandler;
        _currentHealth = _config.PlayerMaxHealth;
        _audioSource = GetComponent<AudioSource>();
    }

    public void TakeAttack(IAttackProcess attackProcess)
    {
        _currentHealth -= attackProcess.Damage;
        _audioSource.Play();
        Debug.Log($"Урон: {attackProcess.Damage}. Осталось: {_currentHealth}");

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        EventManager.Instance.TriggerPlayerDeath();
        Debug.Log("Игрок умер!");
        EventManager.Instance.TriggerMovementLock();
        CharacterController characterController = GetComponent<CharacterController>();
        characterController.enabled = false;
        CapsuleCollider capsuleCollider = GetComponentInChildren<CapsuleCollider>();
        capsuleCollider.enabled = false;
        EventManager.Instance.TriggerInputLock();
        _audioSource.PlayOneShot(_config.DeathClip);
    }

    private void OnGUI()
    {
        var labelStyle = new GUIStyle(GUI.skin.label);
        labelStyle.fontSize = 48; // Укажи нужный размер шрифта
        labelStyle.normal.textColor = Color.white; // Цвет текста

        GUI.Label(new Rect(25,25,350, 200), _currentHealth.ToString(), labelStyle);
    }
}