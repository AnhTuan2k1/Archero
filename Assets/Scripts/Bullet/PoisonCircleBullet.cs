
using UnityEngine;

public class PoisonCircleBullet : CircleBullet
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.CompareTag(TagDefine.Tag_Enemy))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(Damage, DamageType.Poisoned);

            float poisonRate = Player.Instance.PoisonedRate;
            if (poisonRate == 0) poisonRate = PoisonedTouch.POISONED_RATE/2f;
            enemy.Poisoned(Damage * poisonRate);
        }
    }
}
