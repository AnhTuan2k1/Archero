
using UnityEngine;
using System.Threading.Tasks;

public class MultiShot : Ability
{
    private static readonly int DELAY_TIME = 1400;
    public MultiShot() => Id = "MultiShot";

    public override void Active(Bullet bullet)
    {
        Clone((int)(DELAY_TIME / bullet.Speed), bullet);
    }

    private async void Clone(int millisecondsDelay, Bullet bullet)
    {
        await Task.Delay(millisecondsDelay);

        Bullet b = Bullet.InstantiateFromOwn(bullet);
        if (b == null) return;
        b.abilities.Remove(b.abilities.FindLast(a => a.Id == Id));
        b.BulletCreateSound();
        b.ActiveAllAbility();
    }
}
