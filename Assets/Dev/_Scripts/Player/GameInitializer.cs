using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private int _poolSize = 20;

    private void Start()
    {

        ObjectPool objectPool = new GameObject("ObjectPool").AddComponent<ObjectPool>();
        objectPool.Initialize(_projectilePrefab, _poolSize);

        PlayerController playerController = FindObjectOfType<PlayerController>();
        if (playerController == null)
        {
            Debug.LogError("PlayerController не найден!");
            return;
        }

        CharacterController characterController = playerController.GetComponent<CharacterController>();
        Animator animator = playerController.GetComponent<Animator>();

        if (characterController == null || animator == null)
        {
            Debug.LogError("CharacterController или Animator не найдены!");
            return;
        }

        IMovementHandler movementHandler = new MovementHandler(characterController);
        ICameraRotationHandler cameraRotationHandler = new CameraRotationHandler(playerController.transform);
        IInputHandler inputHandler = new InputHandler(animator);
        IAnimatorUpdater animatorUpdater = new AnimatorUpdater(animator, movementHandler, inputHandler);

        playerController.Initialize(movementHandler, cameraRotationHandler, inputHandler, animatorUpdater);
    }
}