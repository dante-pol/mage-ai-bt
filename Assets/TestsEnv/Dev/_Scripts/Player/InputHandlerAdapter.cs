using UnityEngine;
using Root;

public class InputHandlerAdapter : MonoBehaviour, IUltUse
{
    public bool IsUltUse => _inputHandler.IsUlti;
    private Transform _shootPoint;
    private InputHandler _inputHandler;
    private MovementHandler _movementHandler;
    private LightBeamController _lightBeamController;
    private float _leftOffset = 0.1f;
    private AudioSource _audioSource;
    private float _pitchRange = 0.2f;
    private float _minPitch = 0.8f;
    private float _maxPitch = 1.2f;


    public void Setup(Transform shootPoint, InputHandler inputHandler, MovementHandler movementHandler, LightBeamController lightBeam)
    {
        _shootPoint = shootPoint;
        _inputHandler = inputHandler;
        _movementHandler = movementHandler;
        _lightBeamController = lightBeam;

        _audioSource = GetComponent<AudioSource>();
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

    public void TriggerBeamActivation()
    {
        
        _lightBeamController.ActivateBeam();
    }

    public void Step()
    {
        if (_movementHandler != null && IsMoving())
        {
            float randomPitch = Random.Range(_minPitch, _maxPitch);
            _audioSource.pitch = randomPitch;
            
            _audioSource.Play();
        }
    }

    private bool IsMoving()
    {
        return _movementHandler != null && 
            (_movementHandler.GetMoveDirection().magnitude > 0.1f);
    }
}