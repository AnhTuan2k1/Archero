

using System.Linq;
using UnityEngine;

public class DiagonalArrow : Ability
{
    public DiagonalArrow() => Id = "DiagonalArrow";
    public override void Active(Bullet bullet)
    {
        int diagonalArrowQuantity = bullet.abilities.Where(a => a is DiagonalArrow).Count();

        Vector2 vector = bullet.Direction;
        float angleUnit = Mathf.PI / (3 + diagonalArrowQuantity * 2);

        // CloneDiagonalRight
        for (int i = 0; i < diagonalArrowQuantity; i++)
        {
            Bullet b = Bullet.InstantiateFromOwn(bullet);
            float cosAngle = Mathf.Cos(angleUnit * (i + 1));
            float sinAngle = Mathf.Sin(angleUnit * (i + 1));
            b.Direction = new Vector2(vector.x * cosAngle + vector.y * sinAngle, -vector.x * sinAngle + vector.y * cosAngle);
        }
        //CloneDiagonalLeft
        for (int i = 0; i < diagonalArrowQuantity; i++)
        {
            Bullet b = Bullet.InstantiateFromOwn(bullet);
            float cosAngle = Mathf.Cos(angleUnit * (i + 1));
            float sinAngle = Mathf.Sin(angleUnit * (i + 1));
            b.Direction = new Vector2(vector.x * cosAngle - vector.y * sinAngle, vector.x * sinAngle + vector.y * cosAngle);
        }
    }

    private void CloneDiagonalRight(Bullet b)
    {
        Vector2 vectorA = b.Direction;
        float angle = Mathf.PI / 4.0f;
        // Tính toán cos và sin cua góc quay
        float cosAngle = Mathf.Cos(angle);
        float sinAngle = Mathf.Sin(angle);
        Vector2 vectorB = new Vector2(vectorA.x * cosAngle + vectorA.y * sinAngle, -vectorA.x * sinAngle + vectorA.y * cosAngle);

        b.Direction = vectorB;
    }

    private void CloneDiagonalLeft(Bullet b)
    {
        Vector2 vectorA = b.Direction;
        float angle = Mathf.PI / 4.0f;
        float cosAngle = Mathf.Cos(angle);
        float sinAngle = Mathf.Sin(angle);
        Vector2 vectorB = new Vector2(vectorA.x * cosAngle - vectorA.y * sinAngle, vectorA.x * sinAngle + vectorA.y * cosAngle);

        b.Direction = vectorB;
    }
}
