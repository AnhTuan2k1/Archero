

using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class MultiShot : Ability
{
    private static readonly int DELAY_TIME = 1400;
    public MultiShot() => Name = "MultiShot";

    public override void Active(Bullet bullet)
    {
        base.Active(bullet);
        ActiveDuplicatedAbility();
    }

    protected override void ActiveDuplicatedAbility()
    {
        Clone((int)(DELAY_TIME / GetComponent<Bullet>().Speed));
    }

    private async void Clone(int millisecondsDelay)
    {
        Bullet b = Bullet.InstantiateFromOwn(GetComponent<Bullet>());
        Destroy(b.gameObject.GetComponent<MultiShot>());

        b.gameObject.SetActive(false);
        await Task.Delay(millisecondsDelay);
        b?.gameObject.SetActive(true);

        b?.ActiveAllAbility();
    }
}
