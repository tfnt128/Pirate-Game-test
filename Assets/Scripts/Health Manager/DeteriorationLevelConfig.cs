using UnityEngine;

[CreateAssetMenu(fileName = "DeteriorationLevelConfig", menuName = "Custom/Deterioration Level Config", order = 1)]
public class DeteriorationLevelConfig : ScriptableObject
{
    public DeteriorationLevel level;
    public float healthThreshold;
}