using UnityEngine;
using Root;

public class InputHandlerAdapter : MonoBehaviour
{
    private Transform _shootPoint;
    private InputHandler _inputHandler;
    private MovementHandler _movementHandler;
    private LightBeamController _lightBeamController;
    private float _leftOffset = 0.1f;
    private AudioSource _audioSource;
    private float _pitchRange = 0.2f;
    private float _minPitch = 0.8f;
    private float _maxPitch = 1.2f;

    [SerializeField] private AudioClip _clip;


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
        if (_shootPoint == null) return;

        Camera mainCamera = Camera.main;
        if (mainCamera == null) return;

        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f));
        
        Vector3 targetPoint;

        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(100f);

        Vector3 direction = (targetPoint - _shootPoint.position).normalized;

        EventManager.Instance.TriggerAttack(_shootPoint.position, direction);
    }


    public void CallJump()
    {
        _audioSource.PlayOneShot(_clip);
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