using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleAttack : Damager
{
    public GameObject purplePlace;
    private void Awake()
    {
        damage = 80;
        Destroy(gameObject, 3F);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damagable damagable = collision.gameObject.GetComponent<Damagable>();
        if (damagable != null)
        {
            Vector3 offset = collision.transform.position - transform.position;
            collision.transform.position = purplePlace.transform.position + offset * 0.5F;
            DealDamage(damagable);
        }
    }
}
