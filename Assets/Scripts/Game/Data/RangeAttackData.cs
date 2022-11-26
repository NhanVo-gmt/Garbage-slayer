using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/Data/RangeAttackData", fileName = "RangeAttackData")]
public class RangeAttackData : AttackData
{
    [Header("Projectile")]
    public ProjectileData projectileData;
}
