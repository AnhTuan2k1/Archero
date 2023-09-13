

using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public string Name;
    public virtual void Active(Bullet bullet) { }
    protected virtual void ActiveDuplicatedAbility() { }
}


public enum AbilityType
{
    AttackBoost,
    AttackSpeedBoost,
    Blaze,
    BloodThirst,
    BouncyWall,
    CritMaster,
    DiagonalArrow,
    FireCircle,
    FontArrow,
    Freeze,
    HPBoost,
    MultiShot,
    PiercingShot,
    PoisonedTouch,
    Rage,
    RearArrow,
    Ricochet,
    SideArrow

}