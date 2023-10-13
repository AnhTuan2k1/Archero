

using System.Collections.Generic;
using System.Linq;

public class Blaze : Ability
{
    public static readonly float BLAZE_RATE = 0.16f;
    public static readonly float BLAZE_DELAY = 0.25f;
    public static readonly int BLAZE_TIME = 2;

    public Blaze() => Id = "Blaze";

    public static float CalculateBlazeRate(List<AbilityType> abilities)
    {
        int blaze = abilities.Where(a => a is AbilityType.Blaze).Count();
        float rate = blaze * BLAZE_RATE;

        return rate;
    }
}
