using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _maxDistance = 100f;
    [SerializeField] private AudioClip _audioClip;

    [Header("Skin")]
    [SerializeField] private GameObject _mesh;
    [SerializeField] private ParticleSystem _explosionEffect;

    private Collider _collider;

    private float traveledDistance = 0f;
    private Vector3 startingPosition;
    private ObjectPool objectPool;
    private AudioSource _audioSource;
    private AudioClip _currentSource;

    private SpellBallExplosion _explosionSystem;
    private bool _isActive;

    public void SetObjectPool(ObjectPool pool)
    {
        objectPool = pool;
    }

    private void Awake()
    {
        startingPosition = transform.position;
        _audioSource = GetComponent<AudioSource>();

        _collider = GetComponent<Collider>();

        _explosionSystem = new SpellBallExplosion(_explosionEffect, _mesh, this);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        traveledDistance += _speed * Time.deltaTime;

        if (traveledDistance >= _maxDistance && _isActive)
        {
            ReturnToPool();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Попал по врагу");
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Попал по врагу");
            EventManager.Instance.TriggerHitEnemy();
            AudioSource.PlayClipAtPoint(_audioClip, transform.position);
            ReturnToPool();
        }
    }

    private void OnEnable()
    {
        traveledDistance = 0f;
        startingPosition = transform.position;

        _isActive = true;

        _explosionSystem.Reset();

        _collider.enabled = true;
    }

    public void Play()
    {
        _audioSource.Play();
    }

    private void ReturnToPool()
    {
        _collider.enabled = false;

        _isActive = false;

        _explosionSystem.ActiveExplosion(() =>
        {
            objectPool.ReturnObject(gameObject);
        });
    }
}