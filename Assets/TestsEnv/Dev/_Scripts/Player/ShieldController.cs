using UnityEngine;
using Root;
using System.Collections;

public class ShieldController : MonoBehaviour, IEntityAttacked
{
    public Teams TeamID => _teamID;

    [SerializeField] private Teams _teamID;
    [SerializeField] private GameObject _shieldVisual;
    [SerializeField] private Material _shieldMaterial;
    [SerializeField] private Material _damageMaterial;

    private bool _isShieldActive;
    private bool _isOnCooldown;
    private float _cooldownTimer;
    private Color _baseColor;
    private GameConfig _config;
    private InputHandler _inputHandler;


    public void Initialize(GameConfig config, InputHandler inputHandler)
    {
        _config = config;
        _inputHandler = inputHandler;
        _baseColor = _shieldMaterial.color;
        _shieldVisual.SetActive(false);

        EventManager.Instance.OnShieldActivation += ActivateShield;
    }

    public void ActivateShield()
    {
        _shieldVisual.SetActive(true); 
        if (!_isShieldActive && !_isOnCooldown)
        {
            StartCoroutine(ShieldRoutine());
        }
    }

    private IEnumerator ShieldRoutine()
    {
        _isShieldActive = true;
        _shieldVisual.SetActive(true);
        
        yield return new WaitForSeconds(_config.ShieldDuration);
        
        _shieldVisual.SetActive(false);
        _isShieldActive = false;
        
        _inputHandler.EndShield();
    }


    public void TakeAttack(IAttackProcess attackProcess)
    {
        if (_isShieldActive)
        {
            StartCoroutine(BlockEffect());
            Debug.Log("Урон заблокирован щитом!");
        }
        else
        {
            GetComponent<PlayerHealth>().TakeAttack(attackProcess);
        }
    }

    private IEnumerator BlockEffect()
    {
        _shieldMaterial.color = _damageMaterial.color;
        yield return new WaitForSeconds(0.2f);
        _shieldMaterial.color = _baseColor;
    }
}