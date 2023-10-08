
using System.Linq;
using System.Collections.Generic;
using System;

public class AttackSpeedBoost : Ability
{
    private static readonly float ATTACKSPEEDBOOST_RATE = 1.2f;
    public AttackSpeedBoost() => Id = "AttackSpeedBoost";

    public static float CalculateAttackBoostRate(List<AbilityType> abilities)
    {
        int boost = abilities.Where(a => a is AbilityType.AttackSpeedBoost).Count();
        float rate = (float)Math.Pow(ATTACKSPEEDBOOST_RATE, boost);

        return 1/rate;
    }
}
