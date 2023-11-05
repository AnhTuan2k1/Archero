using DG.Tweening;
using UnityEngine;

public class FireCircleBullet : CircleBullet
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.CompareTag(TagDefine.Tag_Enemy))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(Damage, DamageType.Blaze);

            float fireRate = Player.Instance.BlazeRate;
            if (fireRate == 0) fireRate = Blaze.BLAZE_RATE/2f;
            enemy.Burned(Damage * fireRate);

            AudioManager.instance.PlaySound(Sound.Name.FireCol);
        }
    }
}
