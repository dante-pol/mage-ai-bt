using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private int _poolSize = 20;

    [SerializeField] private Transform _shootPoint;

    private int _hitCount = 0;
    private bool _ultiIsAvailiable = false;

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


        IInputHandler inputHandler = new InputHandler(animator);
        IMovementHandler movementHandler = new MovementHandler(characterController, inputHandler);
        ICameraRotationHandler cameraRotationHandler = new CameraRotationHandler(playerTransform, cameraPivot);
        IAnimatorUpdater animatorUpdater = new AnimatorUpdater(animator, movementHandler, inputHandler);

        InputHandlerAdapter adapter = playerController.GetComponentInChildren<InputHandlerAdapter>();
        adapter.Setup(_shootPoint, (InputHandler)inputHandler, (MovementHandler)movementHandler);
        playerController.Initialize(movementHandler, cameraRotationHandler, inputHandler, animatorUpdater);
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

        if (_hitCount >= 1)
        {
            _ultiIsAvailiable = true;
            Debug.Log("Суперспособность доступна");
        }
    }

    private void UseSuperAbility()
    {
        if (_ultiIsAvailiable)
        {   
            Debug.Log("Суперспособность активирована");
            _ultiIsAvailiable = false;
            _hitCount = 0;
        }
    }
}