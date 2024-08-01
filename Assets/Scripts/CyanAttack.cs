using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyanAttack : Damager
{
    private float maxSize = 5;
    private GameObject parent;
    public float lingrowth;
    private void Awake()
    {
        parent = transform.parent.gameObject;
        Destroy(parent, 5F);
    }
    private void FixedUpdate()
    {
        if (parent.transform.localScale.x < maxSize)
        {
            parent.transform.localScale += new Vector3(lingrowth, lingrowth, 0F);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damagable damagable = collision.gameObject.GetComponent<Damagable>();
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (damagable != null && rb != null)
        {
            damage = 80;
            rb.velocity = new Vector2(0, 0);
            DealDamage(damagable, 0.1F);
            damagable.StopMove(2F);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Damagable damagable = collision.gameObject.GetComponent<Damagable>();
        if (damagable != null)
        {
            damage = 7;
            DealDamage(damagable, 0.1F);
        }
    }
}
