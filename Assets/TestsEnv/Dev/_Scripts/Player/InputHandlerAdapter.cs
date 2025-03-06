using UnityEngine;

public class InputHandlerAdapter : MonoBehaviour
{
    private Transform _shootPoint;
    private InputHandler _inputHandler;
    private MovementHandler _movementHandler;
    private float _leftOffset = 0.1f;


    public void Setup(Transform shootPoint, InputHandler inputHandler, MovementHandler movementHandler)
    {
        _shootPoint = shootPoint;
        _inputHandler = inputHandler;
        _movementHandler = movementHandler;
    }


    public void CallHandleAnimationAttack()
    {
        if (_shootPoint != null)
        {
            Camera mainCamera = Camera.main;
            Vector3 direction = mainCamera.transform.forward + (mainCamera.transform.right * -_leftOffset);
            direction.Normalize();
            EventManager.Instance.TriggerAttack(_shootPoint.position, direction);
        }
    }

    public void CallJump()
    {
        _movementHandler.TriggerJump();
    }
}