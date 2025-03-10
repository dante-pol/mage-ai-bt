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
    private AudioSource _audiosource;
    private Material _instanceShieldMaterial;
    private Coroutine _shieldCoroutine;

    public void Initialize(GameConfig config, InputHandler inputHandler)
    {
        _config = config;
        _inputHandler = inputHandler;

        _instanceShieldMaterial = new Material(_shieldMaterial);
        _shieldVisual.GetComponent<Renderer>().material = _instanceShieldMaterial;
        
        _baseColor = _instanceShieldMaterial.color;

        _shieldVisual.SetActive(false);
        _audiosource = GetComponent<AudioSource>();

        EventManager.Instance.OnShieldActivation += ActivateShield;
        _inputHandler.OnShieldDeactivated += HandleShieldDeactivation;
    }

    public void ActivateShield()
    {
        
        if (!_isShieldActive && !_isOnCooldown)
        {
            _instanceShieldMaterial.color = _baseColor;
            _shieldVisual.SetActive(true);
            _shieldCoroutine = StartCoroutine(ShieldRoutine());
        }
    }

    private void HandleShieldDeactivation()
    {
        if (_isShieldActive)
        {
            StopShieldCoroutine();
            DeactivateShield(false);
        }
    }

    private void StopShieldCoroutine()
    {
        if (_shieldCoroutine != null)
        {
            StopCoroutine(_shieldCoroutine);
            _shieldCoroutine = null;
        }
    }

    private IEnumerator ShieldRoutine()
    {
        _isShieldActive = true;
        
        yield return new WaitForSeconds(_config.ShieldDuration);
        
        if (_isShieldActive)
        {
            DeactivateShield(true);
        }
    }

    private void DeactivateShield(bool cooldown)
    {
        _shieldVisual.SetActive(false);
        _instanceShieldMaterial.color = _baseColor;
        _isShieldActive = false;
        _inputHandler.EndShield(cooldown);
    }

    public void TakeAttack(IAttackProcess attackProcess)
    {
        if (_isShieldActive)
        {
            _audiosource.Play();
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
        if(_isShieldActive)
        {
            _instanceShieldMaterial.color = _damageMaterial.color;
            yield return new WaitForSeconds(0.2f);
            
            if(_isShieldActive)
            {
                _instanceShieldMaterial.color = _baseColor;
            }
        }
    }

    private void OnDestroy()
    {
        EventManager.Instance.OnShieldActivation -= ActivateShield;
        if(_instanceShieldMaterial != null)
            Destroy(_instanceShieldMaterial);
    }
}