

using System.Collections.Generic;
using System;
using System.Linq;

public class Rage : Ability
{
    private static readonly int RAGE_RATE = 2;
    public Rage() => Id = "Rage";

    public static float CalculateRageRate(List<AbilityType> abilities, float HpLossRate)
    {
        int rage = abilities.Where(a => a is AbilityType.Rage).Count();
        float rate = rage * (float)Math.Pow(HpLossRate, RAGE_RATE);

        return rate;
    }
}