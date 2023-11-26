using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;

public enum CannonBallType
{
    PlayerProjectile,
    EnemyProjectile,
}

[System.Serializable]
public class ProjectileType
{
    public CannonBallType type;
    public Projectile prefab;
    public int maxSize;
}

public class ProjectilePoolManager : MonoBehaviour
{
    [SerializeField] private List<ProjectileType> projectileTypes;
    private Dictionary<CannonBallType, IObjectPool<Projectile>> _projectilePools;

    private void Awake()
    {
        _projectilePools = new Dictionary<CannonBallType, IObjectPool<Projectile>>();
        InitializePools();
    }

    private void InitializePools()
    {
        foreach (var projectileType in projectileTypes)
        {
            InitializePool(projectileType.type, projectileType.prefab, projectileType.maxSize);
        }
    }

    private void InitializePool(CannonBallType type, Projectile projectilePrefab, int maxSize)
    {
        IObjectPool<Projectile> projectilePool = new ObjectPool<Projectile>(
            () => CreateProjectile(projectilePrefab, type),
            OnGetProjectile,
            OnReleaseProjectile,
            OnDestroyProjectile,
            maxSize : maxSize
        );
        _projectilePools.Add(type, projectilePool);
    }

    private Projectile CreateProjectile(Projectile projectilePrefab, CannonBallType type)
    {
        Projectile projectile = Instantiate(projectilePrefab, Vector3.zero, Quaternion.identity);
        projectile.SetPool(_projectilePools[type]);
        return projectile;
    }

    private void OnGetProjectile(Projectile projectile)
    {
        projectile.gameObject.SetActive(true);
        projectile.transform.position = Vector3.zero;
        projectile.transform.rotation = Quaternion.identity;
    }

    private void OnReleaseProjectile(Projectile projectile)
    {
        projectile.gameObject.SetActive(false);
    }

    private void OnDestroyProjectile(Projectile projectile)
    {
        Destroy(projectile.gameObject);
    }

    public IObjectPool<Projectile> GetProjectilePool(CannonBallType type)
    {
        if (_projectilePools.TryGetValue(type, out var projectilePool))
        {
            return projectilePool;
        }
        else
        {
            Debug.LogError($"ProjectilePool for {type} not found in ProjectilePoolManager.");
            return null;
        }
    }
    public Projectile GetProjectile(CannonBallType type)
    {
        var projectilePool = GetProjectilePool(type);
        return projectilePool?.Get();
    }
}
