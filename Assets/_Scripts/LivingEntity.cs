using UnityEngine;

public interface IDamageable
{
    void ReceiveDamage(
        int damageAmount, RaycastHit hit);
}

public abstract class LivingEntity 
    : MonoBehaviour, IDamageable
{
    [SerializeField]
    protected int health;

    public float GetHealth()
    {
        return health;
    }

    public virtual void ReceiveDamage(
        int damageAmount, RaycastHit hit)
    {
        health -= damageAmount;

        if (health <= 0)
            Die();
    }

    public abstract void Die();
}