using System;
using System.Collections.Generic;
using UnityEngine;

public class ShipDeterioration : MonoBehaviour
{
    [SerializeField] private List<Sprite> deteriorationSprites;
    
    private HealthManager _healthManager;
    private int _currentSpriteIndex = 0;
    private SpriteRenderer _spriteRenderer; 

    private void Awake()
    {
        _healthManager = GetComponent<HealthManager>();
        _healthManager.OnDeterioration += HandleDeterioration;
    }

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void HandleDeterioration()
    {
        if (_currentSpriteIndex < deteriorationSprites.Count)
        {
            _spriteRenderer.sprite = deteriorationSprites[_currentSpriteIndex];
            _currentSpriteIndex += 1;
        }
    }
}
