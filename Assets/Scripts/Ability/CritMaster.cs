
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class CritMaster : Ability
{
    private static readonly float CRIT_RATE = 0.15f;
    public CritMaster() => Id = "CritMaster";

    public static float CalculateCritRate(List<AbilityType> abilities)
    {
        int crit = abilities.Where(a => a is AbilityType.CritMaster).Count();
        float rate = 0;
        for (int i = 0; i < crit; i++)
        {
            rate += CRIT_RATE * (1 - rate);
        }

        return rate;
    }

    public static bool IsCritHappen(float rate)
    {
        return rate*1000 >= Random.Range(1, 1000);
    }
}
