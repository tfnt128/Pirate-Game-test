using UnityEngine;

public class ChaserEnemy : MonoBehaviour
{
    private GenericCharacter _genericCharacter;

    private void Start()
    {
        _genericCharacter = GetComponent<GenericCharacter>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _genericCharacter.healthManager.InstantiateKill();
        }
    }
}
