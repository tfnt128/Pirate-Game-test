using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileType", menuName = "ScriptableObject/Create Projectile Type")]
public class ProjectileTypeData : ScriptableObject
{
    [Tooltip("Projectile Prefab")]
    public Projectile prefab;
    [Tooltip("Pool Max Size")]
    public int maxSize;
}