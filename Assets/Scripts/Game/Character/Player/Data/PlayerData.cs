using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Data/Player/PlayerData", fileName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    public HealthData healthData;

    public DashData dashData;
    public JumpData jumpData;
    public HitData hitData;
    public MoveData moveData;
    public MeleeAttackData meleeAttackData;
    public RangeAttackData rangeAttackData;
}
