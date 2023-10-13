

using System;
using System.Threading.Tasks;
using UnityEngine;

public class Arrow : Bullet
{
    public override ObjectPoolingType BulletType => ObjectPoolingType.Arrow;

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

            PlayerCauseDamageToEnemy(collision);

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

    private void PlayerCauseDamageToEnemy(Collision2D collision)
    {
        GameObject enemy = collision.gameObject;

        bool isDamageCrit = CritMaster.IsCritHappen(Player.Instance.CritRate);
        float damage = owner.Damage * (isDamageCrit ? 2 : 1);
        enemy.GetComponent<Enemy>()
            .TakeDamage(damage, isDamageCrit ? DamageType.Crit : DamageType.Nomal);

        if (Player.Instance.PoisonedRate > 0)
            enemy.GetComponent<Enemy>()
                .Poisoned(owner.Damage * Player.Instance.PoisonedRate);
        if (Player.Instance.BlazeRate > 0)
            enemy.GetComponent<Enemy>()
                .Burned(owner.Damage * Player.Instance.BlazeRate);
        if (Player.Instance.BoltRate > 0)
            Bolt.LightningStrikeEnemies(enemy.transform.position
                , owner.Damage * Player.Instance.BoltRate);
    }

    private async void EnableCollisionAgain(Collider2D collider, int time)
    {
        await Task.Delay(time);
        if (this == null || collider == null) return;
        Physics2D.IgnoreCollision(col, collider, false);
    }

}
