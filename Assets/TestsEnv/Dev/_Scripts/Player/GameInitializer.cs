using UnityEngine;
using System.Collections;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private int _poolSize = 20;
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private ShieldController _shieldController;
    [SerializeField] private PlayerHealth _playerHealth;

    private int _hitCount = 0;
    private const int RequiredHitsForSuperAbility = 10;
    private bool _ultiIsAvailiable = false;
    private bool _isSuperAbilityActive = false;
    private IInputHandler _inputHandler;

    private void Start()
    {
        InitializePlayer();
        InitializeObjectPool();
        SubscribeToEvents();
    }

    private void InitializeObjectPool()
    {
        GameObject poolObject = new GameObject("ObjectPool");
        ObjectPool objectPool = poolObject.AddComponent<ObjectPool>();
        objectPool.Initialize(_projectilePrefab, _poolSize);

        EventManager.Instance.OnAttack += objectPool.ProvideProjectile;
    }

    private void InitializePlayer()
    {
        PlayerController playerController = FindObjectOfType<PlayerController>();
        Transform playerTransform = playerController.transform;
        Transform cameraPivot = playerTransform.Find("Head");
        
        CharacterController characterController = playerController.GetComponent<CharacterController>();
        Animator animator = playerController.GetComponentInChildren<Animator>();

        

        IInputHandler inputHandler = new InputHandler(animator, _gameConfig);
        IMovementHandler movementHandler = new MovementHandler(characterController, inputHandler, _gameConfig);
        ICameraRotationHandler cameraRotationHandler = new CameraRotationHandler(playerTransform, cameraPivot, _gameConfig);
        IAnimatorUpdater animatorUpdater = new AnimatorUpdater(animator, movementHandler, inputHandler);

        LightBeamController beamController = playerController.GetComponentInChildren<LightBeamController>();
        InputHandlerAdapter adapter = playerController.GetComponentInChildren<InputHandlerAdapter>();
        adapter.Setup(_shootPoint, (InputHandler)inputHandler, (MovementHandler)movementHandler, beamController);
        playerController.Initialize(movementHandler, cameraRotationHandler, inputHandler, animatorUpdater);

        ShieldController shieldController = playerController.GetComponentInChildren<ShieldController>();
        shieldController.Initialize(_gameConfig, (InputHandler)inputHandler);
        _playerHealth.Initialize(_gameConfig, (InputHandler)inputHandler);

        _inputHandler = inputHandler;
    }

    private void SubscribeToEvents()
    {
        EventManager.Instance.OnEnemyHit += IncrementHitCount;
        EventManager.Instance.OnSuperAbilityUse += UseSuperAbility;
    }

    private void IncrementHitCount()
    {
        _hitCount++;
        Debug.Log($"Попаданий: {_hitCount}");

        if (_hitCount >= RequiredHitsForSuperAbility)
        {
            _ultiIsAvailiable = true;
            EventManager.Instance.SetSuperAbilityAvailability(true);
            Debug.Log("Суперспособность доступна");
        }
        else
        {
            EventManager.Instance.SetSuperAbilityAvailability(false);
        }
    }

    private void UseSuperAbility()
    {
        if (_ultiIsAvailiable)
        {   
            Debug.Log("Суперспособность активирована");
            _ultiIsAvailiable = false;
            _hitCount = 0;
            EventManager.Instance.SetSuperAbilityAvailability(false);
            StartCoroutine(ActivateSuperAbility());
        }
    }

    private IEnumerator ActivateSuperAbility()
    {
        _isSuperAbilityActive = true;

        EventManager.Instance.TriggerMovementLock();

        EventManager.Instance.TriggerSuperAbilityUse();

        yield return new WaitForSeconds(10f);

        _inputHandler.ResetUlt();

        EventManager.Instance.TriggerSuperAbilityEnd();
        _isSuperAbilityActive = false;

        EventManager.Instance.TriggerMovementUnlock();
    }
}