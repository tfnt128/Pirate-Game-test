using UnityEngine;
using UnityEngine.Pool;

public class Projectile : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private GameObject explosionEffect;
    
    private IObjectPool<Projectile> _projectilePool;
    private TrailRenderer _trailRenderer;

    private void Start()
    {
        _trailRenderer = GetComponentInChildren<TrailRenderer>();
    }

    public void SetPool(IObjectPool<Projectile> pool)
    {
        _projectilePool = pool;
    }

    private void Update()
    {
        MoveProjectile();
    }

    private void MoveProjectile()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        HandleCollision(other);
    }

    private void HandleCollision(Collider2D other)
    {
        if (other.TryGetComponent<GenericCharacter>(out var character))
        {
            character.HealthManager.TakeDamage(1);
        }
        InstantiateExplosionEffect();
        ReleaseProjectile();
    }

    private void InstantiateExplosionEffect()
    {
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
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
            _trailRenderer.Clear();
        }
    }
}