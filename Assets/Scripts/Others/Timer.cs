using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject finalScreen;
    [SerializeField] private TextMeshProUGUI finalScore;

    [Header("Timer Settings")]
    [SerializeField] private float currentTime;
    [SerializeField] private bool countDown;

    [Header("Options Data")]
    public OptionsData optionsData;

    private readonly float _timerLimit = 0f;

    private void Start()
    {
        InitializeTimer();
    }

    private void InitializeTimer()
    {
        Time.timeScale = 1f;
        currentTime = optionsData.gameSessionTime;
    }

    void Update()
    {
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        currentTime = countDown ? currentTime - Time.deltaTime : currentTime + Time.deltaTime;

        if (currentTime <= 1.0f)
        {
            HandleTimerEnd();
        }

        if (currentTime <= _timerLimit)
        {
            SetTimerLimit();
            SetTimerText();
            enabled = false;
        }

        SetTimerText();
    }

    private void HandleTimerEnd()
    {
        finalScore.text = "FINAL SCORE = " + ScoreManager.Instance.ScoreData.points.ToString();
        finalScreen.SetActive(true);
        timerText.color = Color.red;
        Time.timeScale = 0f;
    }

    private void SetTimerLimit()
    {
        currentTime = _timerLimit;
    }

    private void SetTimerText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60F);
        int seconds = Mathf.FloorToInt(currentTime - minutes * 60);
        string formattedTime = string.Format("{0:0}:{1:00}", minutes, seconds);

        timerText.text = formattedTime;
    }
}
