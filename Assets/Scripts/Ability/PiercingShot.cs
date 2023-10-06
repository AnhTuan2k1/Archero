

using System;

public class PiercingShot : Ability
{
    public PiercingShot() => Id = "PiercingShot";

    public void ActivePiercingShot(Bullet bullet)
    {
        bullet.SetVelocity(bullet.Direction.normalized * bullet.Speed);
    }
}
