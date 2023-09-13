

using UnityEngine;

public class BouncyWall : Ability
{
    [SerializeField] private int bouncyTimes;
    public BouncyWall()
    {
        Name = "BouncyWall";
        bouncyTimes = 3;
    }

    public void BulletBounce(Collision2D collision)
    {
        if (bouncyTimes > 0)
        {
            Bullet bullet = gameObject.GetComponent<Bullet>();

            Vector2 newDirection = Vector2.Reflect(bullet.direction, collision.contacts[0].normal);

            bullet.transform.Rotate(Vector3.forward, Vector2.SignedAngle(bullet.direction, newDirection.normalized));
            bullet.direction = newDirection.normalized;
            bullet.SetVelocity(newDirection.normalized * bullet.Speed);

            bouncyTimes--;
            if (bouncyTimes < 1) Destroy(GetComponent<BouncyWall>());
        }
    }

}
