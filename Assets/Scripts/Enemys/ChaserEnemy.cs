using UnityEngine;

[RequireComponent(typeof(GenericCharacter))]
public class ChaserEnemy : MonoBehaviour
{
    private GenericCharacter _genericCharacter;

    private void Start()
    {
        _genericCharacter = GetComponent<GenericCharacter>();
    }
    

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            GenericCharacter playerChar = other.gameObject.GetComponent<GenericCharacter>();
            playerChar.HealthManager.TakeDamage(3);

            _genericCharacter.HealthManager.InstantiateKill(TypeOfDeath.SelfDestroy);
            
        }
    }
}
