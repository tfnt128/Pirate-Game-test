using System;
using UnityEngine;

public class PlayerCannon : MonoBehaviour
{
    [SerializeField] private KeyCode[] fireKeys;
    [SerializeField] private float timeBetweenShots = 0.5f;
    private float _shootCounter;
    private ProjectilePoolManager _cannonManager;

    private void Start()
    {
        _cannonManager = FindObjectOfType<ProjectilePoolManager>();
    }

    void Update()
    {
        _shootCounter -= Time.deltaTime;

        foreach (KeyCode key in fireKeys)
        {
            if (Input.GetKey(key))
            {
                if (_shootCounter <= 0)
                {
                    Projectile projectile = _cannonManager.GetProjectile(CannonBallType.PlayerProjectile);

                    if (projectile != null)
                    {
                        projectile.transform.position = transform.position;
                        projectile.transform.rotation = transform.rotation;

                        _shootCounter = timeBetweenShots;
                    }
                }
            }
        }
    }
}