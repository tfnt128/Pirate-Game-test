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
        if (gameObject.layer != LayerMask.NameToLayer("Player")) ScoreManager.Instance.AddScore(1);

        Instantiate(healthManager.deathEffect, transform.position, quaternion.identity);
        Destroy(gameObject);
    }
}
