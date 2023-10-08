

using System;
using System.Collections.Generic;
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

    public static void AddAbility(AbilityType type, List<Ability> abilities)
    {
        switch (type)
        {
            case AbilityType.AttackBoost:
                //abilities.Add(new AttackBoost());
                break;
            case AbilityType.AttackSpeedBoost:
                //abilities.Add(new AttackSpeedBoost());
                break;
            case AbilityType.Blaze:
                abilities.Add(new Blaze());
                break;
            case AbilityType.BloodThirst:
                //abilities.Add(new BloodThirst());
                break;
            case AbilityType.BouncyWall:
                abilities.Add(new BouncyWall());
                break;
            case AbilityType.CritMaster:
                //abilities.Add(new CritMaster());
                break;
            case AbilityType.DiagonalArrow:
                abilities.Add(new DiagonalArrow());
                break;
            case AbilityType.FireCircle:
                abilities.Add(new FireCircle());
                break;
            case AbilityType.FontArrow:
                abilities.Add(new FontArrow());
                break;
            case AbilityType.Freeze:
                abilities.Add(new Freeze());
                break;
            case AbilityType.HPBoost:
                //abilities.Add(new HPBoost());
                break;
            case AbilityType.MultiShot:
                abilities.Add(new MultiShot());
                break;
            case AbilityType.PiercingShot:
                abilities.Add(new PiercingShot());
                break;
            case AbilityType.PoisonedTouch:
                abilities.Add(new PoisonedTouch());
                break;
            case AbilityType.Rage:
                //abilities.Add(new Rage());
                break;
            case AbilityType.RearArrow:
                abilities.Add(new RearArrow());
                break;
            case AbilityType.Ricochet:
                abilities.Add(new Ricochet());
                break;
            case AbilityType.SideArrow:
                abilities.Add(new SideArrow());
                break;
            default:
                throw new System.Exception("This ability type is unsupported");
        }
    }

    public static void AddAbility(Ability ability, List<Ability> abilities)
    {
        switch (ability)
        {
            case AttackBoost:
                //abilities.Add(new AttackBoost());
                break;
            case AttackSpeedBoost:
                //abilities.Add(new AttackSpeedBoost());
                break;
            case Blaze:
                abilities.Add(new Blaze());
                break;
            case BloodThirst:
                //abilities.Add(new BloodThirst());
                break;
            case BouncyWall:
                abilities.Add(new BouncyWall());
                break;
            case CritMaster:
                //abilities.Add(new CritMaster());
                break;
            case DiagonalArrow:
                abilities.Add(new DiagonalArrow());
                break;
            case FireCircle:
                abilities.Add(new FireCircle());
                break;
            case FontArrow:
                abilities.Add(new FontArrow());
                break;
            case Freeze:
                abilities.Add(new Freeze());
                break;
            case HPBoost:
                //abilities.Add(new HPBoost());
                break;
            case MultiShot:
                abilities.Add(new MultiShot());
                break;
            case PiercingShot:
                abilities.Add(new PiercingShot());
                break;
            case PoisonedTouch:
                abilities.Add(new PoisonedTouch());
                break;
            case Rage:
                //abilities.Add(new Rage());
                break;
            case RearArrow:
                abilities.Add(new RearArrow());
                break;
            case Ricochet:
                abilities.Add(new Ricochet());
                break;
            case SideArrow:
                abilities.Add(new SideArrow());
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
