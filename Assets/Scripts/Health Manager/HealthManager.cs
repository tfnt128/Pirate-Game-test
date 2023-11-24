using System;
using System.Collections.Generic;
using UnityEngine;

public enum DeteriorationLevel
{
    None,
    Level1,
    Level2,
}

public class HealthManager : MonoBehaviour
{
    private DeteriorationLevel _currentDeteriorationLevel = DeteriorationLevel.None;

    [SerializeField] private CharacterHealthData characterHealthData;
    [SerializeField] private List<DeteriorationLevelConfig> deteriorationLevelConfigs;

    public event Action<int> OnHealthChange;
    public event Action OnCharacterDeath;
    public event Action OnDeterioration;
    public GameObject deathEffect;
    private int _health;
    private DateTime _lastDamage;
    private HealthBar _healthBar;
    private bool allDeteriorationDone = false;
    
    public int Health
    {
        get { return _health;}
        set
        {
            if(_health < 0) return;
            
            _health = value;
            OnHealthChange?.Invoke(_health);
            
            if (_health <= 0) OnCharacterDeath?.Invoke();
            
            if(allDeteriorationDone) return;
            
            DeteriorationLevel[] allLevels = (DeteriorationLevel[])Enum.GetValues(typeof(DeteriorationLevel));
            DeteriorationLevel lastLevel = allLevels[allLevels.Length - 1];
            if (_currentDeteriorationLevel == lastLevel)
            {
                allDeteriorationDone = true;
                return;
            }
            
            foreach (var config in deteriorationLevelConfigs)
            {
                if (_health <= characterHealthData.maxHealth * config.healthThreshold &&
                    _currentDeteriorationLevel != config.level)
                {
                    _currentDeteriorationLevel = config.level;
                    OnDeterioration?.Invoke();
                    break;
                }
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
