using Unity.Mathematics;
using UnityEngine;

public class GenericCharacter : MonoBehaviour
{
    internal HealthManager healthManager;

    private void Awake()
    {
        healthManager = GetComponent<HealthManager>();
        healthManager.OnCharacterDeath += HandleDeath;
    }

    private void HandleDeath()
    {
        Instantiate(healthManager.deathEffect, transform.position, quaternion.identity);
        Destroy(gameObject);
    }
}
