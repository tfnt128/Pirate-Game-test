using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileType", menuName = "Create Projectile Type")]
public class ProjectileTypeSO : ScriptableObject
{
    public Projectile prefab;
    public int maxSize;
}