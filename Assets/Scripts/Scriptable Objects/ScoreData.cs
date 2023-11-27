using UnityEngine;

[CreateAssetMenu(fileName = "ScoreData", menuName = "ScriptableObject/Score Data")]
public class ScoreData : ScriptableObject
{
    [Tooltip("Normal Points")]
    public int points;
    [Tooltip("High Score")]
    public int highScorePoints;
}
