using UnityEngine;

public class InputHandlerAdapter : MonoBehaviour
{
    private Transform _shootPoint;
    private InputHandler _inputHandler;


    public void Setup(Transform shootPoint, InputHandler inputHandler)
    {
        _shootPoint = shootPoint;
        _inputHandler = inputHandler;
    }


    public void CallHandleAnimationAttack()
    {
        if (_shootPoint != null)
        {
            Camera mainCamera = Camera.main;
            Vector3 direction = mainCamera.transform.forward;
            EventManager.Instance.TriggerAttack(_shootPoint.position, direction);
        }
    }
}