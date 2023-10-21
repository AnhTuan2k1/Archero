using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : Bullet
{
    public override ObjectPoolingType BulletType => ObjectPoolingType.FireBullet;

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.CompareTag(TagDefine.Tag_Player))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(Damage);
            Die();
        }
        else if (collision.gameObject.CompareTag(TagDefine.Tag_Enemy))
        {
            Physics2D.IgnoreCollision(col, collision.collider, true);
            this.Velocity = Direction.normalized * Speed;
        }
    }

    public override void BulletCreateSound()
    {
        //base.BulletCreateSound();
    }
}
