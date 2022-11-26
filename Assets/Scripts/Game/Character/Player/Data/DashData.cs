using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Data/Player/DashData", fileName = "DashData")]
public class DashData : ScriptableObject
{
    public float initialVelocity;
    public float cooldown;

    public PooledObjectData vfx;
}
