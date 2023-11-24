using System;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private CharacterHealthData characterHealthData;

    public event Action<int> OnHealthChange;
    public event Action OnCharacterDeath;
    public GameObject deathEffect;
    private int _health;
    private DateTime _lastDamage;
    private HealthBar _healthBar;

    public int Health
    {
        get { return _health;}
        set
        {
            if(_health < 0) return;
            _health = value;
            Debug.Log(_health);
            OnHealthChange?.Invoke(_health);
            if (_health <= 0)
            {
                OnCharacterDeath?.Invoke();
            }
        }
    }

    private void Start()
    {
        _healthBar = GetComponentInChildren<HealthBar>();
        Health = characterHealthData.maxHealth;
    }
    

    public bool TakeDamage(int value)
    {
        if (!CanTakeDamage()) return false;
        Health -= value;
        _healthBar.UpdateHealthBar(characterHealthData.maxHealth, Health);
        _lastDamage = DateTime.UtcNow;
        return true;
    }
    public bool InstantiateKill()
    {
        if (!CanTakeDamage()) return false;
        Health -= Health;
        return true;
    }

    private bool CanTakeDamage()
    {
        if (!characterHealthData.canTurnOnIFrames) return true;
        if (characterHealthData.invincibleFramesTimer > 0)
        {
            TimeSpan timeSpan = DateTime.UtcNow - _lastDamage;
            return timeSpan.TotalSeconds > characterHealthData.invincibleFramesTimer;
        }

        return true;
    }
}
