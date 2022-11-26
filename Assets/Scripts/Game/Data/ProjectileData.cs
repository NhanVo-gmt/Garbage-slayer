using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileData", menuName = "ScriptableObjects/Data/ProjectileData")]
public class ProjectileData : PooledObjectData
{
    [Header("Projectile")]
    public int damage;
    public float velocity;
}
