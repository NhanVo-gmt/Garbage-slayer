using UnityEngine;

public class GameSettings : SingletonObject<GameSettings>
{
    protected override void Awake()
    {
        base.Awake();
    }

    [Header("Player")]
    public int maxHealth = 10;

    [Header("KnockBack")]
    public float WeakKnockbackAmount = 200;
    
}
