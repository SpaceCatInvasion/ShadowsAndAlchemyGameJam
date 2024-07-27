using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Damagable
{
    
    private void Awake()
    {
        health = 25;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Damagable player = collision.gameObject.GetComponent<Damagable>();
            if (player != null)
            {
                player.TakeDamage(1);
            }
        }
    }
}
