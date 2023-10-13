

using System.Collections.Generic;
using System.Linq;

public class BoltCircle : Ability
{
    public BoltCircle() => Id = "BoltCircle";

    public static int CalculateBoltCircle(List<AbilityType> abilities)
    {
        int bolt = abilities.Where(a => a is AbilityType.BoltCircle).Count();

        return bolt;
    }
}
