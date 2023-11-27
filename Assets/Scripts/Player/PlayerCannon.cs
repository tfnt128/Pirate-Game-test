using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerCannonConfiguration
{
    public Transform cannonSelected;
    public KeyCode[] fireKeys;
    public float timeBetweenShots = 0.5f;
}

public class PlayerCannon : MonoBehaviour
{
    [Header("Projectile Type")] 
    [SerializeField] private ProjectileTypeSO type;
    
    [Header("Configurations")] 
    [SerializeField] private List<PlayerCannonConfiguration> playerCannonConfigurations;

    private Dictionary<Transform, float> _shootCounters = new Dictionary<Transform, float>();
    private ProjectilePoolManager _cannonManager;

    private void Start()
    {
        _cannonManager = FindObjectOfType<ProjectilePoolManager>();

        InitializeShootCounters();
    }

    private void InitializeShootCounters()
    {
        foreach (var config in playerCannonConfigurations)
        {
            _shootCounters[config.cannonSelected] = 0f;
        }
    }

    void Update()
    {
        foreach (var config in playerCannonConfigurations)
        {
            UpdateShooting(config);
        }
    }

    private void UpdateShooting(PlayerCannonConfiguration config)
    {
        _shootCounters[config.cannonSelected] -= Time.deltaTime;
        
        foreach (KeyCode key in config.fireKeys)
        {
            if (_shootCounters[config.cannonSelected] <= 0)
            {


                if (Input.GetKey(key))
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
    }

}
