

using System.Collections.Generic;
using System.Linq;

public class PoisonCircle : Ability
{
    public PoisonCircle() => Id = "PoisonCircle";

    public static int CalculatePoisonCircle(List<AbilityType> abilities)
    {
        int poison = abilities.Where(a => a is AbilityType.PoisonCircle).Count();

        return poison;
    }
}
