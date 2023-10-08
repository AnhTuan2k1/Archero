
using System.Linq;
using System.Collections.Generic;
using System;

public class AttackBoost : Ability
{
    private static readonly float ATTACKBOOST_RATE = 1.25f;
    public AttackBoost() => Id = "AttackBoost";

    public static float CalculateAttackBoostRate(List<AbilityType> abilities)
    {
        int boost = abilities.Where(a => a is AbilityType.AttackBoost).Count();
        float rate = (float)Math.Pow(ATTACKBOOST_RATE, boost);

        return rate;
    }
}
