using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private Queue<GameObject> _pool;
    private GameObject _prefab;
    private Transform _poolContainer;

    public void Initialize(GameObject prefab, int poolSize)
    {
        _prefab = prefab;
        _pool = new Queue<GameObject>();
        
        _poolContainer = new GameObject("PoolContainer").transform;
        _poolContainer.SetParent(transform);

        for (int i = 0; i < poolSize; i++)
        {
            CreateNewObject();
        }
    }

    private GameObject CreateNewObject()
    {
        GameObject obj = Instantiate(_prefab, _poolContainer);
        obj.SetActive(false);
        obj.GetComponent<ProjectileBehaviour>().SetObjectPool(this);
        _pool.Enqueue(obj);
        return obj;
    }

    public void ProvideProjectile(Vector3 position, Vector3 direction)
    {
        GameObject projectile;
        
        if (_pool.Count == 0)
        {
            projectile = CreateNewObject();
        }
        else
        {
            projectile = _pool.Dequeue();
        }

        projectile.transform.position = position;
        projectile.transform.rotation = Quaternion.LookRotation(direction);
        projectile.SetActive(true);

        projectile.GetComponent<ProjectileBehaviour>().Play();
    }

    public void ReturnObject(GameObject obj)
    {
        if (obj != null && _pool != null)
        {
            obj.SetActive(false);
            obj.transform.SetParent(_poolContainer);
            _pool.Enqueue(obj);
        }
    }
}