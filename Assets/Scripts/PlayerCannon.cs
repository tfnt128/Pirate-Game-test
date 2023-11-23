using UnityEngine;
using UnityEngine.Pool;

public class PlayerCannon : MonoBehaviour
{
    private float _shootCounter;
    private IObjectPool<Projectile> _projectilePool;

    [SerializeField] private Projectile ballCannon;
    [SerializeField] private Transform cannonPosition;
    [SerializeField] private float timeBetweenShots = .5f;

    private void Awake()
    {
        _projectilePool = new ObjectPool<Projectile>(
            CreateProjectile,
            OnGet,
            OnRealease,
            OnDestroyProjectile,
            maxSize: 3
        );
    }

    private Projectile CreateProjectile()
    {
        Projectile projectile = Instantiate(ballCannon, cannonPosition.position, cannonPosition.rotation);
        projectile.SetPool(_projectilePool);
        return projectile;
    }

    private void OnGet(Projectile projectile)
    {
        projectile.gameObject.SetActive(true);
        projectile.transform.position = cannonPosition.position;
        projectile.transform.rotation = cannonPosition.rotation;
    }

    private void OnRealease(Projectile projectile)
    {
        projectile.gameObject.SetActive(false);
    }

    private void OnDestroyProjectile(Projectile projectile)
    {
        Destroy(projectile.gameObject);
    }

    void Update()
    {
        _shootCounter -= Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            if (_shootCounter <= 0)
            {
                _projectilePool.Get();

                _shootCounter = timeBetweenShots;
            }
        }
    }
}