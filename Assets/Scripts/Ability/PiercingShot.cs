
public class PiercingShot : Ability
{
    public PiercingShot() => Id = "PiercingShot";

    public void ActivePiercingShot(Bullet bullet)
    {
        bullet.Velocity = bullet.Direction.normalized * bullet.Speed;
    }
}
