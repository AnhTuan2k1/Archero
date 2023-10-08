

using System;
using System.Threading.Tasks;
using UnityEngine;

public class Arrow : Bullet
{
    public Arrow() => direction = Vector2.right;

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            BouncyWall bouncy = (BouncyWall)abilities.FindLast(a => a is BouncyWall);
            if (bouncy != null) bouncy.BulletBounce(collision, this);
            else Die(1000);
        }

        else if (collision.gameObject.CompareTag("Enemy") && this.Owner is Player)
        {
            Physics2D.IgnoreCollision(col, collision.collider, true);
            EnableCollisionAgain(collision.collider, 50);

            collision.gameObject.GetComponent<Enemy>().TakeDamage(this.Owner);

            Ricochet ricochet = (Ricochet)abilities.FindLast(a => a is Ricochet);
            PiercingShot piercingShot = (PiercingShot)abilities.FindLast(a => a is PiercingShot);

            if (ricochet != null)
            {
                bool isAcvited = ricochet.ActiveRicochet(collision.transform.position, this);
                if (isAcvited == false)
                {
                    if (piercingShot != null)
                    {
                        piercingShot.ActivePiercingShot(this);
                    }
                    else Die();
                }
            }
            else if (piercingShot != null)
            {
                piercingShot.ActivePiercingShot(this);
            }
            else Die();
        }
    }

    private async void EnableCollisionAgain(Collider2D collider, int time)
    {
        await Task.Delay(time);
        if (this == null || collider == null) return;
        Physics2D.IgnoreCollision(col, collider, false);
    }

}
