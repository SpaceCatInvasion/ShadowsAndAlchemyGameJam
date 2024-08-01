using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Damagable
{
    private Vector2 dir = new Vector2(0,0);
    private Vector3 movedir = new Vector3(0,0,0);
    private Rigidbody2D rb;
    private void Awake()
    {
        health = 200;
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Damagable player = collision.gameObject.GetComponent<Damagable>();
            if (player != null)
            {
                player.TakeDamage(1);
                dir *= -0.2F;
                movedir *= -0.2F;
            }
        }
    }
    private void Update()
    {
        movedir = -(gameObject.transform.position - EnemyManager.instance.player.transform.position);
        if (immunityFrames > 0)
        {
            immunityFrames -= Time.deltaTime;
        }
    }
    private void FixedUpdate()
    {
        if (canmove)
        {
            dir = (dir * EnemyManager.instance.enemySlow + new Vector2(movedir.x, movedir.y)).normalized * EnemyManager.instance.enemySpeed;
            rb.velocity = dir;
        }
        else
        {
            dir = rb.velocity;
        }
    }
}
