

using System.Collections.Generic;
using System.Linq;

public class BloodThirst : Ability
{
    private static readonly float BLOODTHIRST_RATE = 0.15f;
    public BloodThirst() => Id = "BloodThirst";

    public static float CalculateBloodThirstRate(List<AbilityType> abilities)
    {
        int blood = abilities.Where(a => a is AbilityType.BloodThirst).Count();
        float rate = 0;
        for (int i = 0; i < blood; i++)
        {
            rate += BLOODTHIRST_RATE * (1 - rate);
        }

        return rate*0.2f;
    }
}
