using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Damager : MonoBehaviour
{
    protected float damage;
    protected virtual void DealDamage(Damagable damagable)
    {
        damagable.TakeDamage(damage);
    }
    protected virtual void DealDamage(Damagable damagable, float immunity)
    {
        if (damagable.TakeDamage(damage))
        {
            damagable.SetImmunityFrames(immunity);
        }
    }
}
