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

    [Header("Options Settings")]
    public float gameSessionTime = 60f;
    public float enemySpawnRate = 2f;
    
    private readonly float _minGameSessionTime = 60f;

    private bool didChangeSomenthing;
    
    private static OptionsManager _instance;

    public static OptionsManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<OptionsManager>();

                if (_instance == null)
                {
                    GameObject singleton = new GameObject("OptionsManager");
                    _instance = singleton.AddComponent<OptionsManager>();
                }
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        gameSessionTimeSlider.value = (gameSessionTime - _minGameSessionTime) / (180f - _minGameSessionTime); 
        enemySpawnRateSlider.value = (enemySpawnRate - 2f) / 8f; 
        UpdateTexts();
    }
    

    public void SaveOptions()
    {
        gameSessionTime = gameSessionTimeSlider.value * (180f - _minGameSessionTime) + _minGameSessionTime; 
        enemySpawnRate = enemySpawnRateSlider.value * 8f + 2f; 
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