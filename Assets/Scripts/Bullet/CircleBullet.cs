

using UnityEngine;

public static class TagDefine
{
    public static readonly string Tag_Player = "Player";
    public static readonly string Tag_Enemy = "Enemy";
    public static readonly string Tag_Bullet = "Bullet";
    public static readonly string Tag_Wall = "Wall";
}

public class CircleBullet : Bullet
{
    public override ObjectPoolingType BulletType => ObjectPoolingType.CircleBullet;

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if(collision.gameObject.CompareTag(TagDefine.Tag_Player))
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
