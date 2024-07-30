using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowAttack : Damager
{
    private void Awake()
    {
        damage = 5;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Damagable damagable = collision.gameObject.GetComponent<Damagable>();
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (damagable != null && rb != null)
        {
            rb.velocity *= 0.25F;
            DealDamage(damagable);
            damagable.StopMove(0.05F);
        }
        Destroy(gameObject, 0.05F);
    }
}
