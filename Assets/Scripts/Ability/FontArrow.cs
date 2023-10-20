
using System.Linq;
using UnityEngine;

public class FontArrow : Ability
{
    public static readonly float DISTANCE = 0.25f;
    public FontArrow() => Id = "FontArrow";

    public override void Active(Bullet bullet)
    {
        int fontArrowLength = bullet.abilities.Where(a => a is FontArrow).Count();

        Vector2 pointA = bullet.transform.position;
        Vector2 n = new Vector2(-bullet.Direction.y, bullet.Direction.x).normalized * DISTANCE;

        for (int i = 1; i <= fontArrowLength; i++)
        {
            Bullet b = Bullet.InstantiateFromOwn(bullet);
            b.gameObject.transform.position = pointA + n * (i - fontArrowLength / 2f);
        }

        bullet.gameObject.transform.position = pointA - n * fontArrowLength / 2f;
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
