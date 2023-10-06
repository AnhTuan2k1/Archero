

using System;
using System.Linq;
using UnityEngine;

public class RearArrow : Ability
{
    public RearArrow() => Id = "RearArrow";
    public override void Active(Bullet bullet)
    {
        int rearArrowLength = bullet.abilities.Where(a => a is RearArrow).Count();
        if (rearArrowLength == 1)
        {
            CloneBehind(bullet);
        }
        else if (rearArrowLength > 1)
        {
            CloneFrontBullet(CloneBehind(bullet), rearArrowLength);
        }
    }

    private Bullet CloneBehind(Bullet bullet)
    {
        Bullet b = Bullet.InstantiateFromOwn(bullet);
        b.Direction = -bullet.Direction;
        return b;
    }

    private void CloneFrontBullet(Bullet Own, int rearArrowLength)
    {
        Vector2 pointA = Own.transform.position;
        Vector2 n = new Vector2(-Own.Direction.y, Own.Direction.x).normalized * FontArrow.DISTANCE;

        for (int i = 1; i < rearArrowLength; i++)
        {
            Bullet b = Bullet.InstantiateFromOwn(Own);
            b.gameObject.transform.position = pointA + n * (i - (rearArrowLength - 1) / 2f);
        }

        Own.gameObject.transform.position = pointA - n * (rearArrowLength - 1) / 2f;
    }
}
