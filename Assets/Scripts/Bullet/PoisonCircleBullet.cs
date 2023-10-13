using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonCircleBullet : Bullet
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
            enemy.TakeDamage(owner.Damage * 1.5f, DamageType.Poisoned);

            float poisonRate = Player.Instance.BlazeRate;
            if (poisonRate == 0) poisonRate = PoisonedTouch.POISONED_RATE;
            enemy.Poisoned(owner.Damage * poisonRate);
        }
    }
}
