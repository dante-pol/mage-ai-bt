using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private Queue<GameObject> _pool;
    private GameObject _prefab;

    public void Initialize(GameObject prefab, int poolSize)
    {
        this._prefab = prefab;
        _pool = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            _pool.Enqueue(obj);
        }

        EventManager.Instance.OnAttack += ProvideProjectile;
    }

    private void ProvideProjectile(Vector3 position, Vector3 direction)
    {
        if (_pool.Count == 0)
        {
            InstantiateProjectile(position, direction);
            return;
        }

        GameObject projectile = _pool.Dequeue();
        projectile.transform.position = position;
        projectile.transform.rotation = Quaternion.LookRotation(direction);
        projectile.SetActive(true);

        ProjectileBehaviour projectileBehaviour = projectile.GetComponent<ProjectileBehaviour>();
        if (projectileBehaviour != null)
        {
            projectileBehaviour.SetObjectPool(this);
        }
    }

    private void InstantiateProjectile(Vector3 position, Vector3 direction)
    {
        GameObject obj = Instantiate(_prefab, position, Quaternion.LookRotation(direction));
        obj.SetActive(true);

        ProjectileBehaviour projectileBehaviour = obj.GetComponent<ProjectileBehaviour>();
        if (projectileBehaviour != null)
        {
            projectileBehaviour.SetObjectPool(this);
        }
    }

    public void ReturnObject(GameObject obj)
    {
        if (obj != null && _pool != null)
        {
            obj.SetActive(false);
            _pool.Enqueue(obj);
        }
    }
}