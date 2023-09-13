

using System;
using UnityEngine;

public class RearArrow : Ability
{
    public RearArrow() => Name = "RearArrow";
    public override void Active(Bullet bullet)
    {
        ActiveDuplicatedAbility();
    }

    protected override void ActiveDuplicatedAbility()
    {
        RearArrow[] rearArrows = GetComponents<RearArrow>();
        if (rearArrows.Length == 1)
        {
            CloneBehind();
        }
        else if (rearArrows.Length > 1)
        {
            CloneFrontBullet(CloneBehind());
        }
    }

    private Bullet CloneBehind()
    {
        Bullet b = Bullet.InstantiateFromOwn(GetComponent<Bullet>());
        b.direction = -b.direction;
        return b;
    }

    private void CloneFrontBullet(Bullet Own)
    {
        RearArrow[] rearArrows = Own.GetComponents<RearArrow>();

        Vector2 pointA = Own.transform.position;
        Vector2 n = new Vector2(-Own.direction.y, Own.direction.x).normalized * FontArrow.DISTANCE;

        for (int i = 1; i < rearArrows.Length; i++)
        {
            Bullet b = Bullet.InstantiateFromOwn(Own);
            b.gameObject.transform.position = pointA + n * (i - (rearArrows.Length - 1) / 2f);
        }

        Own.gameObject.transform.position = pointA - n * (rearArrows.Length - 1) / 2f;
    }
}
