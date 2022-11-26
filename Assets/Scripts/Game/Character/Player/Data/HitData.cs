using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Data/Player/HitData", fileName = "HitData")]
public class HitData : ScriptableObject
{
    public float blinkCooldown;
    public float invulnerableTime;
}
