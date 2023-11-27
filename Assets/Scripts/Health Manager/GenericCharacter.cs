using Unity.Mathematics;
using UnityEngine;

public enum CharacterType
{
    Player,
    Enemy
}
[RequireComponent(typeof(HealthManager))]
public class GenericCharacter : MonoBehaviour
{
    [SerializeField] private CharacterType characterType;
    internal HealthManager HealthManager;

    private void Awake()
    {
        HealthManager = GetComponent<HealthManager>();
        HealthManager.OnCharacterDeath += (sender, e) => HandleDeath(e.DeathType);
    }

    private void HandleDeath(TypeOfDeath typeOfDeath)
    {
        if (characterType == CharacterType.Enemy && typeOfDeath == TypeOfDeath.Normal)
        {
            ScoreManager.Instance.AddScore(1);
        }
        
        Instantiate(HealthManager.DeathEffect, transform.position, quaternion.identity);
        Destroy(gameObject);
    }
}
