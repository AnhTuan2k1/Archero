using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltCircleBullet : Bullet
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
            enemy.TakeDamage(owner.Damage * 1.5f, DamageType.Bolt);

            float boltRate = Player.Instance.BoltRate;
            if (boltRate == 0) boltRate = Bolt.BOLT_RATE/2 ;

            Bolt.LightningStrikeEnemies(enemy.transform.position, owner.Damage * boltRate);
        }
    }
}
