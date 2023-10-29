

using System;
using System.Collections.Generic;
using System.Linq;

public class HPBoost : Ability
{
    private static readonly float HPBOOST_RATE = 1.35f;
    public HPBoost() => Id = "HPBoost";

    public static float CalculateHPBoostRate(List<AbilityType> abilities)
    {
        int boost = abilities.Where(a => a is AbilityType.HPBoost).Count();
        float rate = (float)Math.Pow(HPBOOST_RATE, boost);

        return rate;
    }
}
