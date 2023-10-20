using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCircleBullet : Bullet
{
    private void Start()
    {
        owner = Player.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(Damage * 1.5f, DamageType.Blaze);

            float fireRate = Player.Instance.BlazeRate;
            if (fireRate == 0) fireRate = Blaze.BLAZE_RATE/2f;
            enemy.Burned(Damage * fireRate);
        }
    }
}
