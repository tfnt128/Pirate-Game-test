using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CannonConfiguration
{
    public Transform cannonSelected;
    public float timeBetweenShots = 1f;
}

[RequireComponent(typeof(GenericEnemyFollow))]
[RequireComponent(typeof(AITargetDetector))]
public class ShooterEnemy : MonoBehaviour
{
    [Header("Projectile Type")] 
    [SerializeField] private ProjectileTypeData type;
    
    [Header("Configurations")] 
    [SerializeField] private List<CannonConfiguration> cannonConfigurations;

    private AITargetDetector _playerDetector;
    private GenericEnemyFollow _enemyFollow;
    private Dictionary<Transform, float> _shootCounters = new Dictionary<Transform, float>();
    private ProjectilePoolManager _cannonManager;

    private void Start()
    {
        _playerDetector = GetComponent<AITargetDetector>();
        _enemyFollow = GetComponent<GenericEnemyFollow>();
        _cannonManager = FindObjectOfType<ProjectilePoolManager>();

        InitializeShootCounters();
    }

    private void InitializeShootCounters()
    {
        foreach (var config in cannonConfigurations)
        {
            _shootCounters[config.cannonSelected] = 0f;
        }
    }

    void Update()
    {
        foreach (var config in cannonConfigurations)
        {
            UpdateShooting(config);
        }
    }

    private void UpdateShooting(CannonConfiguration config)
    {
        _shootCounters[config.cannonSelected] -= Time.deltaTime;

        if (_playerDetector.IsPlayerDetected)
        {
            _enemyFollow.CanPursue = false;

            if (_shootCounters[config.cannonSelected] <= 0)
            {
                if (_cannonManager != null)
                {
                    Projectile projectile = _cannonManager.GetProjectile(type);
                
                    if (projectile != null)
                    {
                        projectile.transform.position = config.cannonSelected.position;
                        projectile.transform.rotation = config.cannonSelected.rotation;
                        projectile.gameObject.SetActive(true);

                        _shootCounters[config.cannonSelected] = config.timeBetweenShots;
                    }
                }
            }
            
        }
        else
        {
            _enemyFollow.CanPursue = true;
        }
    }
}
