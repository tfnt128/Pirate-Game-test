using UnityEngine;

[CreateAssetMenu(fileName = "DeteriorationLevelConfig", menuName = "ScriptableObject/Deterioration Level Config", order = 1)]
public class DeteriorationLevelConfig : ScriptableObject
{
    public DeteriorationLevel level;
    public float healthThreshold;
}