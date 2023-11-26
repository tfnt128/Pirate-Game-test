using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    [Header("UI Components")]
    public Slider gameSessionTimeSlider;
    public Slider enemySpawnRateSlider;
    public TextMeshProUGUI gameSessionTimeText;
    public TextMeshProUGUI enemySpawnRateText;

    [Header("Options Data")]
    public OptionsData optionsData;

    private readonly float _minGameSessionTime = 60f;

    private void Start()
    {
        optionsData.gameSessionTime = 60f;
        optionsData.enemySpawnRate = 2f;
        
        gameSessionTimeSlider.value = (optionsData.gameSessionTime - _minGameSessionTime) / (180f - _minGameSessionTime); 
        enemySpawnRateSlider.value = (optionsData.enemySpawnRate - 2f) / 8f;

        LoadOptions();
        UpdateUI();
    }

    public void SaveOptions()
    {
        optionsData.gameSessionTime = gameSessionTimeSlider.value * (180f - _minGameSessionTime) + _minGameSessionTime;
        optionsData.enemySpawnRate = enemySpawnRateSlider.value * 8f + 2f;
    }
    
    
    private void LoadOptions()
    {
        gameSessionTimeSlider.value = (optionsData.gameSessionTime - _minGameSessionTime) / (180f - _minGameSessionTime);
        enemySpawnRateSlider.value = (optionsData.enemySpawnRate - 2f) / 8f;
    }

    private void UpdateUI()
    {
        UpdateTexts();
    }

    private void UpdateTexts()
    {
        gameSessionTimeText.text = $"Game Session Time: {Mathf.Round(gameSessionTimeSlider.value * (180f - _minGameSessionTime) + _minGameSessionTime)} seconds";
        enemySpawnRateText.text = $"Enemy Spawn Rate: {Mathf.Round((enemySpawnRateSlider.value * 8f) + 2f)} seconds";
    }

    public void OnGameSessionTimeSliderChanged()
    {
        UpdateTexts();
    }

    public void OnEnemySpawnRateSliderChanged()
    {
        UpdateTexts();
    }
}