using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    protected float health = 100;
    protected float immunityFrames = 0;
    public virtual void TakeDamage(float damage)
    {
        if (immunityFrames <= 0)
        {
            health -= damage;
            if (health <= 0)
            {
                Die();
            }
        }
    }
    public virtual void Heal(int healHp)
    {
        health += healHp;
    }
    public virtual void SetImmunityFrames(int frames)
    {
        immunityFrames = frames;
    }
    protected virtual void Die()
    {
        EnemyManager.instance.enemiesDead++;
        Destroy(gameObject);
    }
}
