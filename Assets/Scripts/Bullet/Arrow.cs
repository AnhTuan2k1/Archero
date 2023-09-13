

using System;
using UnityEngine;

public class Arrow : Bullet
{
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);        

        if (collision.gameObject.CompareTag("Enemy") && this.owner is Player)
        {
            Physics2D.IgnoreCollision(col, collision.collider, true);
            collision.gameObject.GetComponent<Enemy>().TakeDamage(this.owner);

            Ricochet ricochet = GetComponent<Ricochet>();
            PiercingShot piercingShot = GetComponent<PiercingShot>();

            if (ricochet != null)
            {
                bool isAcvited = ricochet.ActiveRicochet(collision.transform.position);
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

    protected override void OnCollisionExit2D(Collision2D collision)
    {
        base.OnCollisionExit2D(collision);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Physics2D.IgnoreCollision(col, collision.collider, false);
        }
    }
}
