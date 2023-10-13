
using System.Collections.Generic;
using System.Linq;

public class PoisonedTouch : Ability
{
    public static readonly float POISONED_RATE = 0.34f;
    public static readonly float POISONED_DELAY = 1.2f;
    public PoisonedTouch() => Id = "PoisonedTouch";

    public static float CalculatePoisonedRate(List<AbilityType> abilities)
    {
        int poisoned = abilities.Where(a => a is AbilityType.PoisonedTouch).Count();
        float rate = poisoned * POISONED_RATE;

        return rate;
    }
}
