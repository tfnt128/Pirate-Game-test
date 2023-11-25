using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    internal HealthManager healthManager;
    void Start()
    {
        healthManager = GetComponent<HealthManager>();
        healthManager.OnCharacterDeath += HandleDeath;
    }

    private void HandleDeath()
    {
        ScoreManager.Instance.AddScore(1);
    }

}
