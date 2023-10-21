using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceFireBullet : Bullet
{
    public override ObjectPoolingType BulletType => ObjectPoolingType.BounceFireBullet;

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            BouncyWall bouncy = (BouncyWall)abilities.FindLast(a => a is BouncyWall);
            if (bouncy != null) bouncy.BulletBounce(collision, this);
            else Die();
        }
        else if (collision.gameObject.CompareTag(TagDefine.Tag_Player))
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
