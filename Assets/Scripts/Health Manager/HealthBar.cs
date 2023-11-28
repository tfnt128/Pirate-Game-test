using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthBar : MonoBehaviour
{
    [Header("Health Bar Components")]
    [SerializeField] private Image healthBarSprite;
    [SerializeField] private GameObject healthBarCanvas;

    [Header("Animation Duration")]
    [SerializeField] private float duration;

    private float _currentFillAmount;

    private void Start()
    {
        _currentFillAmount = healthBarSprite != null ? healthBarSprite.fillAmount : 0f;
    }

    public void UpdateHealthBar(int maxHealth, float newCurrentHealth)
    {
        float newFillAmount = newCurrentHealth / maxHealth;

        DOTween.To(() => _currentFillAmount, x => _currentFillAmount = x, newFillAmount, duration)
            .OnUpdate(() =>
            {
                if (healthBarSprite != null)
                {
                    healthBarSprite.fillAmount = _currentFillAmount;
                }
            });
    }

    private void LateUpdate()
    {
        healthBarCanvas.transform.position = transform.position + Vector3.up * 1f;
        
        healthBarCanvas.transform.rotation = Quaternion.identity;
    }
}