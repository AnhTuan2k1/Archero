
using System.Collections.Generic;
using UnityEngine;

public class FontArrow : Ability
{
    public static readonly float DISTANCE = 0.25f;
    public FontArrow() => Name = "FontArrow";

    public override void Active(Bullet bullet)
    {
        base.Active(bullet);
        ActiveDuplicatedAbility();
    }

    protected override void ActiveDuplicatedAbility()
    {
        Bullet bullet = GetComponent<Bullet>();
        FontArrow[] fontArrows = bullet.GetComponents<FontArrow>();

        Vector2 pointA = bullet.transform.position;
        Vector2 n = new Vector2(-bullet.direction.y, bullet.direction.x).normalized * DISTANCE;

        for (int i = 1; i <= fontArrows.Length; i++)
        {
            Bullet b = Bullet.InstantiateFromOwn(bullet);
            b.gameObject.transform.position = pointA + n * (i - fontArrows.Length / 2f);
        }

        bullet.gameObject.transform.position = pointA - n * fontArrows.Length / 2f;
    }


    //private void CloneFront()
    //{
    //    Bullet b = Bullet.InstantiateFromOwn(bullet);

    //    Vector2 pointA = b.transform.position;
    //    Vector2 n = new Vector2(-b.direction.y, b.direction.x).normalized * DISTANCE;

    //    Vector2 pointC = pointA + n;
    //    Vector2 pointD = pointA - n;

    //    bullet.gameObject.transform.position = pointD;
    //    b.gameObject.transform.position = pointC;
    //}
}
