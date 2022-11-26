using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Data/AttackData", fileName = "Attack Data")]
public class AttackData : ScriptableObject
{
    [Header("Damage")]
    public int damage;
    public float coolDown;

    [Header("VFX")]
    public PooledObjectData vfx;
}
