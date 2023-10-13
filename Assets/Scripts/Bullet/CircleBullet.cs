

using UnityEngine;

public class CircleBullet : Bullet
{
    public override ObjectPoolingType BulletType => ObjectPoolingType.CircleBullet;

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(Damage);
            Die();
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Physics2D.IgnoreCollision(col, collision.collider, true);
            this.Velocity = Direction.normalized * speed;
        }
    }

    public override void BulletCreateSound()
    {
        //base.BulletCreateSound();
    }

    //protected override void Start()
    //{
    //    base.Start();
    //    IgnoreCollideWithOthersEnemy();
    //}

    //private void IgnoreCollideWithOthersEnemy()
    //{
    //    GameManager.Instance.observers.ForEach(observer => {
    //        if (observer is Enemy e)
    //        {
    //            if (e.col != col)
    //            {
    //                Physics2D.IgnoreCollision(col, e.col, true);
    //            }
    //        }
    //    });
    //}
}
