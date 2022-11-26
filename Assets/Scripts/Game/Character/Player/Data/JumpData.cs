using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Data/Player/JumpData", fileName = "JumpData")]
public class JumpData : ScriptableObject
{
    [Header("Velocity")]
    public float velocity;

    public PooledObjectData jumpVFX;
}
