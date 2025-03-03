using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _maxDistance = 100f;

    private float traveledDistance = 0f;
    private Vector3 startingPosition;
    private ObjectPool objectPool;


    public void SetObjectPool(ObjectPool pool)
    {
        objectPool = pool;
    }

    private void Start()
    {
        startingPosition = transform.position;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        traveledDistance += _speed * Time.deltaTime;

        if (traveledDistance >= _maxDistance)
        {
            ReturnToPool();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Попал по врагу");
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Попал по врагу");
            EventManager.Instance.TriggerHitEnemy();
            ReturnToPool();
        }
    }

    private void OnEnable()
    {
        traveledDistance = 0f;
        startingPosition = transform.position;
    }

    private void ReturnToPool()
    {
        objectPool.ReturnObject(gameObject);
    }
}