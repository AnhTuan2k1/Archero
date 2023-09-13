

using UnityEngine;

public class DiagonalArrow : Ability
{
    public DiagonalArrow() => Name = "DiagonalArrow";
    public override void Active(Bullet bullet)
    {
        ActiveDuplicatedAbility();
    }

    protected override void ActiveDuplicatedAbility()
    {
        Bullet bullet = GetComponent<Bullet>();
        DiagonalArrow[] diagonalArrows = bullet.GetComponents<DiagonalArrow>();

        Vector2 vector = bullet.direction;
        float angleUnit = Mathf.PI /(3 + diagonalArrows.Length*2);

        // CloneDiagonalRight
        for (int i = 0; i < diagonalArrows.Length; i++)
        {
            Bullet b = Bullet.InstantiateFromOwn(bullet);
            float cosAngle = Mathf.Cos(angleUnit * (i + 1));
            float sinAngle = Mathf.Sin(angleUnit * (i + 1));
            b.direction = new Vector2(vector.x * cosAngle + vector.y * sinAngle, -vector.x * sinAngle + vector.y * cosAngle);
        }
        //CloneDiagonalLeft
        for (int i = 0; i < diagonalArrows.Length; i++)
        {
            Bullet b = Bullet.InstantiateFromOwn(bullet);
            float cosAngle = Mathf.Cos(angleUnit * (i + 1));
            float sinAngle = Mathf.Sin(angleUnit * (i + 1));
            b.direction = new Vector2(vector.x * cosAngle - vector.y * sinAngle, vector.x * sinAngle + vector.y * cosAngle);
        }
    }

    private void CloneDiagonalRight()
    {
        Bullet b = Bullet.InstantiateFromOwn(GetComponent<Bullet>());

        Vector2 vectorA = b.direction;
        float angle = Mathf.PI / 4.0f;
        // Tính toán cos và sin cua góc quay
        float cosAngle = Mathf.Cos(angle);
        float sinAngle = Mathf.Sin(angle);
        Vector2 vectorB = new Vector2(vectorA.x * cosAngle + vectorA.y * sinAngle, -vectorA.x * sinAngle + vectorA.y * cosAngle);

        b.direction = vectorB;
    }

    private void CloneDiagonalLeft()
    {
        Bullet b = Bullet.InstantiateFromOwn(GetComponent<Bullet>());

        Vector2 vectorA = b.direction;
        float angle = Mathf.PI / 4.0f;
        float cosAngle = Mathf.Cos(angle);
        float sinAngle = Mathf.Sin(angle);
        Vector2 vectorB = new Vector2(vectorA.x * cosAngle - vectorA.y * sinAngle, vectorA.x * sinAngle + vectorA.y * cosAngle);

        b.direction = vectorB;
    }
}
