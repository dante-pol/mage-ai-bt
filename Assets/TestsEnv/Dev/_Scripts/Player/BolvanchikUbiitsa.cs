using UnityEngine;

public class BolvanchikUbiitsa : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float bulletForce = 20f;
    [SerializeField] private GameObject bulletPrefab;

    private float nextFireTime;

    void Start()
    {
        if (firePoint == null)
        {
            Debug.LogError("Fire Point не назначен!");
        }
    }

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);

    }
}