using UnityEngine;

public class ShooterEnemy : MonoBehaviour
{
    private AIPlayerDetector _playerDetector;
    private GenericEnemyFollow _enemyFollow;

    private float _shootCounter;
    private ProjectilePoolManager _cannonManager;

    [SerializeField] private float timeBetweenShots = 1f;
    
    private void Start()
    {
        _playerDetector = GetComponentInParent<AIPlayerDetector>();
        _enemyFollow = GetComponentInParent<GenericEnemyFollow>();
        _cannonManager = FindObjectOfType<ProjectilePoolManager>();
    }
    

    void Update()
    {
        _shootCounter -= Time.deltaTime;
        if (_playerDetector.PlayerDetected)
        {
            _enemyFollow.canPursue = false;
            if (_shootCounter <= 0)
            {
                if (_cannonManager != null)
                {
                    Projectile projectile = _cannonManager.GetProjectile(CannonBallType.EnemyProjectile);
                    if (projectile != null)
                    {
                        projectile.transform.position = transform.position;
                        projectile.transform.rotation = transform.rotation;
                        projectile.gameObject.SetActive(true);
                        
                        _shootCounter = timeBetweenShots;
                    }
                }
            }
        }
        else
        {
            _enemyFollow.canPursue = true;
        }
    }
}