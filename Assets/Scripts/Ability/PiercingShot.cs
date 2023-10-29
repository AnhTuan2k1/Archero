
public class PiercingShot : Ability
{
    private int piercingTimes;
    public PiercingShot()
    {
        Id = "PiercingShot";
        piercingTimes = 2;
    }

    public void ActivePiercingShot(Bullet bullet)
    {
        if (piercingTimes > 0)
        {
            bullet.Velocity = bullet.Direction.normalized * bullet.Speed;

            piercingTimes--;
            if (piercingTimes < 1) bullet.abilities.Remove(this);
        }
    }
}
