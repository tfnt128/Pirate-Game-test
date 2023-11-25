using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textGame;
    private TextMeshProUGUI _textFinalScreen;

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

    public int _currentPoints { get; private set; }

    private void Start()
    {
        _currentPoints = 0;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        textGame.text = "POINTS " + _currentPoints.ToString();
    }

    public void AddScore(int points)
    {
        _currentPoints += points;
        UpdateScoreText();
    }
}