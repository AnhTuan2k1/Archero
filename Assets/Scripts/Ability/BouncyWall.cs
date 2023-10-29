

using UnityEngine;

public class BouncyWall : Ability
{
    private int bouncyTimes;
    public BouncyWall()
    {
        Id = "BouncyWall";
        bouncyTimes = 2;
    }

    public void BulletBounce(Collision2D collision, Bullet bullet)
    {
        if (bouncyTimes > 0)
        {
            Vector2 newDirection = Vector2.Reflect(bullet.Direction, collision.contacts[0].normal);
            bullet.Direction = newDirection.normalized;

            bouncyTimes--;
            if (bouncyTimes < 1) bullet.abilities.Remove(this);
        }
    }

}
