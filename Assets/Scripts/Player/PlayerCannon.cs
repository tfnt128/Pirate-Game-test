using UnityEngine;
using UnityEngine.Pool;

public class PlayerCannon : MonoBehaviour
{
    private float _shootCounter;
    private IObjectPool<Projectile> _projectilePool;

    [SerializeField] private Projectile ballCannon;
    [SerializeField] private float timeBetweenShots = .5f;

    [SerializeField] private KeyCode fireKey;

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
        Projectile projectile = Instantiate(ballCannon, transform.position, transform.rotation);
        projectile.SetPool(_projectilePool);
        return projectile;
    }

    private void OnGet(Projectile projectile)
    {
        projectile.gameObject.SetActive(true);
        projectile.transform.position = transform.position;
        projectile.transform.rotation = transform.rotation;
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
        if (Input.GetKey(fireKey))
        {
            if (_shootCounter <= 0)
            {
                _projectilePool.Get();

                _shootCounter = timeBetweenShots;
            }
        }
    }
}