using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{


    [Header("Text Components")]
    [SerializeField] private TextMeshProUGUI pointsText;
    
    [Header("Reset or not the Score")]
    [SerializeField] private bool resetScoreAfterLoad;

    [Header("Score Data")]
    [SerializeField] private ScoreData scoreData;

    public ScoreData ScoreData
    {
        get { return scoreData; }
        private set { scoreData = value; }
    }
    
    private static ScoreManager _instance;

    public static ScoreManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ScoreManager>();

                if (_instance == null)
                {
                    Debug.LogError("ScoreManager not found in the scene. Make sure it is added to a GameObject in your scene.");
                }
            }

            return _instance;
        }
    }

    private void Start()
    {
        pointsText.text = scoreData.points.ToString();
        InitializeScore();
    }

    private void InitializeScore()
    {
        if(resetScoreAfterLoad) scoreData.points = 0;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        pointsText.text = "POINTS " + scoreData.points.ToString();
    }

    public void AddScore(int points)
    {
        scoreData.points += points;
        UpdateScoreText();
    }
}