

using UnityEngine;

public class BouncyWall : Ability
{
    [SerializeField] private int bouncyTimes;
    public BouncyWall()
    {
        Id = "BouncyWall";
        bouncyTimes = 3;
    }

    public void BulletBounce(Collision2D collision, Bullet bullet)
    {
        if (bouncyTimes > 0)
        {
            Vector2 newDirection = Vector2.Reflect(bullet.Direction, collision.contacts[0].normal);

            //bullet.transform.Rotate(Vector3.forward, Vector2.SignedAngle(bullet.Direction, newDirection.normalized));
            bullet.Direction = newDirection.normalized;
            //bullet.SetVelocity(newDirection.normalized * bullet.Speed);

            bouncyTimes--;
            if (bouncyTimes < 1) bullet.abilities.Remove(this);
        }
    }

}
