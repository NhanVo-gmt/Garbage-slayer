using UnityEngine;

public interface IDamageable
{
    public enum DamagerTarget
    {
        Player,
        Enemy,
        Trap
    }

    public enum KnockbackType
    {
        none,
        weak
    }
    
    public DamagerTarget GetDamagerType();
    public KnockbackType GetKnockbackType();
    public void TakeDamage(int damage, DamagerTarget damagerType, Vector2 attackDirection);
}
