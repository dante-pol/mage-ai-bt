using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private int _poolSize = 20;

    private void Start()
    {
        PlayerController playerController = FindObjectOfType<PlayerController>();
        Transform playerTransform = playerController.transform;
        Transform cameraPivot = playerTransform.Find("Head");

        CharacterController characterController = playerController.GetComponent<CharacterController>();
        Animator animator = playerController.GetComponent<Animator>();


        IInputHandler inputHandler = new InputHandler(animator);
        IMovementHandler movementHandler = new MovementHandler(characterController, inputHandler);
        ICameraRotationHandler cameraRotationHandler = new CameraRotationHandler(playerTransform, cameraPivot);
        IAnimatorUpdater animatorUpdater = new AnimatorUpdater(animator, movementHandler, inputHandler);


        playerController.Initialize(movementHandler, cameraRotationHandler, inputHandler, animatorUpdater);
    }
}