using UnityEngine;

[CreateAssetMenu(fileName = "OptionsData", menuName = "ScriptableObject/Game Session and Enemy Spawn User Data", order = 1)]
public class OptionsData : ScriptableObject
{
    public float gameSessionTime = 60f;
    public float enemySpawnRate = 2f;
}