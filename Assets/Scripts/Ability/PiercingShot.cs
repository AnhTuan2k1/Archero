

using System;

public class PiercingShot : Ability
{
    public PiercingShot() => Name = "PiercingShot";

    public void ActivePiercingShot(Bullet bullet)
    {
        bullet.SetVelocity(bullet.direction.normalized * bullet.Speed);
    }
}
