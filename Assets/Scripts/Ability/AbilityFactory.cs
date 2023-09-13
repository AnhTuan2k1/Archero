

using System;
using UnityEngine;

public class AbilityFactory
{
    [SerializeField] private Ability[] abilities;
    private static AbilityFactory _instance;
    public static AbilityFactory Instance
    {
        get 
        {
            if (_instance == null)
                _instance = new AbilityFactory();
            return _instance;
        }
    }

    public static void AddAbility(AbilityType type, GameObject gameObject)
    {
        switch (type)
        {
            case AbilityType.AttackBoost:
                gameObject.AddComponent<AttackBoost>();
                break;
            case AbilityType.AttackSpeedBoost:
                gameObject.AddComponent<AttackSpeedBoost>();
                break;
            case AbilityType.Blaze:
                gameObject.AddComponent<Blaze>();
                break;
            case AbilityType.BloodThirst:
                gameObject.AddComponent<BloodThirst>();
                break;
            case AbilityType.BouncyWall:
                gameObject.AddComponent<BouncyWall>();
                break;
            case AbilityType.CritMaster:
                gameObject.AddComponent<CritMaster>();
                break;
            case AbilityType.DiagonalArrow:
                gameObject.AddComponent<DiagonalArrow>();
                break;
            case AbilityType.FireCircle:
                gameObject.AddComponent<FireCircle>();
                break;
            case AbilityType.FontArrow:
                gameObject.AddComponent<FontArrow>();
                break;
            case AbilityType.Freeze:
                gameObject.AddComponent<Freeze>();
                break;
            case AbilityType.HPBoost:
                gameObject.AddComponent<HPBoost>();
                break;
            case AbilityType.MultiShot:
                gameObject.AddComponent<MultiShot>();
                break;
            case AbilityType.PiercingShot:
                gameObject.AddComponent<PiercingShot>();
                break;
            case AbilityType.PoisonedTouch:
                gameObject.AddComponent<PoisonedTouch>();
                break;
            case AbilityType.Rage:
                gameObject.AddComponent<Rage>();
                break;
            case AbilityType.RearArrow:
                gameObject.AddComponent<RearArrow>();
                break;
            case AbilityType.Ricochet:
                gameObject.AddComponent<Ricochet>();
                break;
            case AbilityType.SideArrow:
                gameObject.AddComponent<SideArrow>();
                break;
            default:
                throw new System.Exception("This ability type is unsupported");
        }
    }

    //public static Ability GetAbility(AbilityType abilityType)
    //{
    //    return abilityType switch
    //    {
    //        AbilityType.AttackBoost => new AttackBoost(),
    //        AbilityType.AttackSpeedBoost => new AttackSpeedBoost(),
    //        AbilityType.Blaze => new Blaze(),
    //        AbilityType.BloodThirst => new BloodThirst(),
    //        AbilityType.BouncyWall => new BouncyWall(),
    //        AbilityType.CritMaster => new CritMaster(),
    //        AbilityType.DiagonalArrow => new DiagonalArrow(),
    //        AbilityType.FireCircle => new FireCircle(),
    //        AbilityType.FontArrow => new FontArrow(),
    //        AbilityType.Freeze => new Freeze(),
    //        AbilityType.HPBoost => new HPBoost(),
    //        AbilityType.MultiShot => new MultiShot(),
    //        AbilityType.PiercingShot => new PiercingShot(),
    //        AbilityType.PoisonedTouch => new PoisonedTouch(),
    //        AbilityType.Rage => new Rage(),
    //        AbilityType.RearArrow => new RearArrow(),
    //        AbilityType.Ricochet => new Ricochet(),
    //        AbilityType.SideArrow => new SideArrow(),
    //        _ => throw new System.Exception("This ability type is unsupported"),
    //    };
    //}
}
