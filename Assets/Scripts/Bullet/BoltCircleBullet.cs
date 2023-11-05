using DG.Tweening;
using UnityEngine;

public class BoltCircleBullet : CircleBullet
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.CompareTag(TagDefine.Tag_Enemy))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(Damage, DamageType.Bolt);

            if (!enemy.isActiveAndEnabled) return;

            float boltRate = Player.Instance.BoltRate;
            if (boltRate == 0) boltRate = Bolt.BOLT_RATE / 2;
            Bolt.LightningStrikeEnemies(enemy.transform.position, Damage * boltRate);
        }
    }
}
