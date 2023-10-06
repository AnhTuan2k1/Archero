
using System.Linq;
using UnityEngine;

public class SideArrow : Ability
{
    public SideArrow() => Id = "SideArrow";
    public override void Active(Bullet bullet)
    {
        int sideArrowLength = bullet.abilities.Where(a => a is SideArrow).Count();
        if (sideArrowLength == 1)
        {
            CloneRight(bullet);
            CloneLeft(bullet);
        }
        else if (sideArrowLength > 1)
        {
            CloneFrontBullet(CloneRight(bullet), sideArrowLength);
            CloneFrontBullet(CloneLeft(bullet), sideArrowLength);
        }
    }

    private Bullet CloneRight(Bullet bullet)
    {
        Bullet b = Bullet.InstantiateFromOwn(bullet);
        b.Direction = new Vector2(b.Direction.y, -b.Direction.x);
        return b;
    }

    private Bullet CloneLeft(Bullet bullet)
    {
        Bullet b = Bullet.InstantiateFromOwn(bullet);
        b.Direction = new Vector2(-b.Direction.y, b.Direction.x);
        return b;
    }

    private void CloneFrontBullet(Bullet Own, int sideArrowLength)
    {

        Vector2 pointA = Own.transform.position;
        Vector2 n = new Vector2(-Own.Direction.y, Own.Direction.x).normalized * FontArrow.DISTANCE;

        for (int i = 1; i < sideArrowLength; i++)
        {
            Bullet b = Bullet.InstantiateFromOwn(Own);
            b.gameObject.transform.position = pointA + n * (i - (sideArrowLength - 1) / 2f);
        }

        Own.gameObject.transform.position = pointA - n * (sideArrowLength - 1) / 2f;
    }
}
