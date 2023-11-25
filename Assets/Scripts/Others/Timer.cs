using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject finalScreen;
    [SerializeField] private TextMeshProUGUI finalScore;

    [Header("Timer Settings")]
    [SerializeField] private float currentTime;
    [SerializeField] private bool countDown;
    
    private readonly float _timerLimit = 0;

    private void Start()
    {
        currentTime = OptionsManager.Instance.gameSessionTime;
    }

    void Update()
    {
        currentTime = countDown ? currentTime -= Time.deltaTime : currentTime += Time.deltaTime;

        if (currentTime <= 1.0f)
        {
            finalScore.text = "FINAL SCORE = " + ScoreManager.Instance._currentPoints;
            finalScreen.SetActive(true);
            timerText.color = Color.red;
        }
        
        if (currentTime <= _timerLimit)
        {
            currentTime = _timerLimit;
            SetTimerText();
            enabled = false;
        }
        SetTimerText();
    }
    private void SetTimerText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60F);
        int seconds = Mathf.FloorToInt(currentTime - minutes * 60);
        string formattedTime = string.Format("{0:0}:{1:00}", minutes, seconds);

        timerText.text = formattedTime;
    }
}