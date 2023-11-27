using UnityEngine;

[CreateAssetMenu(fileName = "OptionsData", menuName = "ScriptableObject/Game Session and Enemy Spawn User Data")]
public class OptionsData : ScriptableObject
{
    [Tooltip("Game Session Time")]
    public float gameSessionTime = 60f;
    [Tooltip("Enemy Spawn Rate")]
    public float enemySpawnRate = 2f;
}