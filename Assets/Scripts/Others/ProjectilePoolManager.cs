using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;


public class ProjectilePoolManager : MonoBehaviour
{
    [SerializeField] private List<ProjectileTypeData> projectileTypes;
    private Dictionary<ProjectileTypeData, IObjectPool<Projectile>> _projectilePools;

    private void Awake()
    {
        _projectilePools = new Dictionary<ProjectileTypeData, IObjectPool<Projectile>>();
        InitializePools();
    }

    private void InitializePools()
    {
        foreach (var projectileType in projectileTypes)
        {
            InitializePool(projectileType);
        }
    }

    private void InitializePool(ProjectileTypeData projectileType)
    {
        IObjectPool<Projectile> projectilePool = new ObjectPool<Projectile>(
            () => CreateProjectile(projectileType),
            OnGetProjectile,
            OnReleaseProjectile,
            OnDestroyProjectile,
            maxSize: projectileType.maxSize
        );
        _projectilePools.Add(projectileType, projectilePool);
    }

    private Projectile CreateProjectile(ProjectileTypeData projectileType)
    {
        Projectile projectile = Instantiate(projectileType.prefab, Vector3.zero, Quaternion.identity);
        projectile.SetPool(_projectilePools[projectileType]);
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

    public IObjectPool<Projectile> GetProjectilePool(ProjectileTypeData type)
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

    public Projectile GetProjectile(ProjectileTypeData type)
    {
        var projectilePool = GetProjectilePool(type);
        return projectilePool?.Get();
    }
}
