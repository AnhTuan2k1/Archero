

using UnityEngine;

//[System.Serializable]
public abstract class Ability
{
    public string Id;
    public virtual void Active(Bullet bullet) {}
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
    SideArrow,
    Bolt,
    BoltCircle,
    PoisonCircle

}