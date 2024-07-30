using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenAttack : Damager
{
    private void Awake()
    {
        damage = 10;
        Destroy(gameObject, 2F);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damagable damagable = collision.gameObject.GetComponent<Damagable>();
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (damagable != null && rb != null)
        {
            rb.velocity = new Vector2(0, 0);
            DealDamage(damagable);
            damagable.StopMove(2F);
        }
    }
}

