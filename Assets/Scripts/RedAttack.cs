using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedAttack : Damager
{
    private float knockback=10;
    private void Awake()
    {
        damage = 40;
        Destroy(gameObject, 0.5F);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damagable damagable = collision.gameObject.GetComponent<Damagable>();
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (damagable != null && rb != null)
        {
            float angle = (transform.rotation.eulerAngles.z - 90) * Mathf.Deg2Rad;
            rb.velocity = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * knockback;
            damage = 40;
            DealDamage(damagable);
            damagable.StopMove(0.2F);
        }
    }
}
