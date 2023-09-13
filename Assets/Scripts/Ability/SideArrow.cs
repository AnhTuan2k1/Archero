
using UnityEngine;

public class SideArrow : Ability
{
    public SideArrow() => Name = "SideArrow";
    public override void Active(Bullet bullet)
    {
        ActiveDuplicatedAbility();
    }

    protected override void ActiveDuplicatedAbility()
    {
        SideArrow[] sideArrows = GetComponent<Bullet>().GetComponents<SideArrow>();
        if (sideArrows.Length == 1)
        {
            CloneRight();
            CloneLeft();
        }
        else if(sideArrows.Length > 1)
        {
            CloneFrontBullet(CloneRight());
            CloneFrontBullet(CloneLeft());
        }
    }

    private Bullet CloneRight()
    {
        Bullet b = Bullet.InstantiateFromOwn(GetComponent<Bullet>());
        b.direction = new Vector2(b.direction.y, -b.direction.x);
        return b;
    }

    private Bullet CloneLeft()
    {
        Bullet b = Bullet.InstantiateFromOwn(GetComponent<Bullet>());
        b.direction = new Vector2(-b.direction.y, b.direction.x);
        return b;
    }

    private void CloneFrontBullet(Bullet Own)
    {
        SideArrow[] fontArrows = Own.GetComponents<SideArrow>();

        Vector2 pointA = Own.transform.position;
        Vector2 n = new Vector2(-Own.direction.y, Own.direction.x).normalized * FontArrow.DISTANCE;

        for (int i = 1; i < fontArrows.Length; i++)
        {
            Bullet b = Bullet.InstantiateFromOwn(Own);
            b.gameObject.transform.position = pointA + n * (i - (fontArrows.Length - 1) / 2f);
        }

        Own.gameObject.transform.position = pointA - n * (fontArrows.Length - 1) / 2f;
    }
}
