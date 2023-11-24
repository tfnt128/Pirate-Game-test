using UnityEngine;

[CreateAssetMenu(fileName = "Char_", menuName = "ScriptableObject/Character")]
public class CharacterHealthData : ScriptableObject
{
    [Tooltip("Max Character Health")]
    public int maxHealth;
    [Tooltip("Invincible Frames Time")]
    public float invincibleFramesTimer;
    [Tooltip("Make character invincible for a period of time equal to the 'InvincibleFramesTimer' attribute")]
    public bool canTurnOnIFrames = true;
}
