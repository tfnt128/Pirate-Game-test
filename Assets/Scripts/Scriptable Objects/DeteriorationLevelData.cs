using UnityEngine;

[CreateAssetMenu(fileName = "DeteriorationLevelConfig", menuName = "ScriptableObject/Deterioration Level Config")]
public class DeteriorationLevelData : ScriptableObject
{
    [Tooltip("Deterioration Level")]
    public DeteriorationLevel level;
    [Tooltip("The health threshold for the ship to increase the deterioration level")]
    public float healthThreshold;
}