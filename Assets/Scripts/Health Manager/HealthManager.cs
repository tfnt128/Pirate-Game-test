using System;
using System.Collections.Generic;
using UnityEngine;

public enum DeteriorationLevel
{
    None,
    Level1,
    Level2
}

public enum TypeOfDeath
{
    SelfDestroy,
    Normal
}

public class DeathEventArgs : EventArgs
{
    public TypeOfDeath DeathType { get; }
    public GameObject Character { get; }

    public DeathEventArgs(TypeOfDeath deathType, GameObject character)
    {
        DeathType = deathType;
        Character = character;
    }
    
    public static DeathEventArgs Create(TypeOfDeath deathType, GameObject character)
    {
        return new DeathEventArgs(deathType, character);
    }
}

[RequireComponent(typeof(HealthBar))]
[RequireComponent(typeof(GenericCharacter))]
[RequireComponent(typeof(ShipDeterioration))]
public class HealthManager : MonoBehaviour
{
    public event Action<int> OnHealthChange;
    public event EventHandler<DeathEventArgs> OnCharacterDeath;
    public event Action OnDeterioration;
    public GameObject DeathEffect
    {
        get { return deathEffect; }
        private set { deathEffect = value; }
    }

    [Header("Character Data")]
    [SerializeField] private CharacterHealthData healthData;
    [SerializeField] private List<DeteriorationLevelData> deteriorationLevelConfigs;

    [Header("Character Death Effect")]
    [SerializeField] private GameObject deathEffect;

    private DeteriorationLevel _currentDeteriorationLevel = DeteriorationLevel.None;

    private int _health;
    private DateTime _lastDamageTime;
    private HealthBar _healthBar;
    private bool _allDeteriorationDone;

    public int Health
    {
        get { return _health; }
        set
        {
            if (_health < 0) return;

            _health = value;
            OnHealthChange?.Invoke(_health);

            if (_health <= 0) DeathTypeHandler(TypeOfDeath.Normal);

            if (_allDeteriorationDone) return;

            CheckDeterioration();
        }
    }

    private void Start()
    {
        _healthBar = GetComponentInChildren<HealthBar>();
        Health = healthData.maxHealth;
    }

    private void CheckDeterioration()
    {
        if (_allDeteriorationDone) return;

        foreach (var config in deteriorationLevelConfigs)
        {
            if (_health <= healthData.maxHealth * config.healthThreshold &&
                _currentDeteriorationLevel != config.level)
            {
                _currentDeteriorationLevel = config.level;
                OnDeterioration?.Invoke();
                break;
            }
        }

        UpdateAllDeteriorationFlag();
    }

    private void UpdateAllDeteriorationFlag()
    {
        DeteriorationLevel[] allLevels = (DeteriorationLevel[])Enum.GetValues(typeof(DeteriorationLevel));
        DeteriorationLevel lastLevel = allLevels[allLevels.Length - 1];

        if (_currentDeteriorationLevel == lastLevel)
        {
            _allDeteriorationDone = true;
        }
    }

    private void DeathTypeHandler(TypeOfDeath typeOfDeath)
    {
        OnCharacterDeath?.Invoke(this, DeathEventArgs.Create(typeOfDeath, gameObject));
    }

    public bool TakeDamage(int value)
    {
        if (!CanTakeDamage()) return false;

        Health -= value;
        _healthBar?.UpdateHealthBar(healthData.maxHealth, _health);
        _lastDamageTime = DateTime.UtcNow;
        return true;
    }

    public bool InstantiateKill(TypeOfDeath typeOfDeath)
    {
        if (!CanTakeDamage()) return false;

        DeathTypeHandler(typeOfDeath);

        return true;
    }

    private bool CanTakeDamage()
    {
        if (!healthData.canTurnOnIFrames) return true;

        if (healthData.invincibleFramesTimer > 0)
        {
            TimeSpan timeSpan = DateTime.UtcNow - _lastDamageTime;
            return timeSpan.TotalSeconds > healthData.invincibleFramesTimer;
        }

        return true;
    }
}
