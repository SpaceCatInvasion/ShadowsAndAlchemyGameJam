using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : Damager
{
    private void Awake()
    {
        damage = 10;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        Damagable objectHit = collision.gameObject.GetComponent<Damagable>();
        if(objectHit != null)
        {
            DealDamage(objectHit);
        }
    }
}
