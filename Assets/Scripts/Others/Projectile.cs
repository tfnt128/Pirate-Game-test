using UnityEngine;
using UnityEngine.Pool;

public class Projectile : MonoBehaviour
{
    private IObjectPool<Projectile> _projectilePool;

    private TrailRenderer _tr;

    [SerializeField] private float speed = 5;

    [SerializeField] private GameObject explosionEffect;


    private void Start()
    {
        _tr = GetComponentInChildren<TrailRenderer>();
    }

    public void SetPool(IObjectPool<Projectile> pool)
    {
        _projectilePool = pool;
    }


    private void Update()
    {
        transform.position += (Vector3)(transform.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<GenericCharacter>(out var enemy))
        {
            enemy.healthManager.TakeDamage(1);
        }
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        ReleaseProjectile();
    }

    private void OnBecameInvisible()
    {
        ReleaseProjectile();
    }

    private void ReleaseProjectile()
    {
        if (gameObject.activeSelf)
        {
            _projectilePool.Release(this);
            _tr.Clear();
        }
    }
}