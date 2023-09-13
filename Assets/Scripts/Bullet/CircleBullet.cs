

using UnityEngine;

public class CircleBullet : Bullet
{
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(this.owner);
            Die();
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Physics2D.IgnoreCollision(col, collision.collider, true);
            SetVelocity(direction.normalized * speed);
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
