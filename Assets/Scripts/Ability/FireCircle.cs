

using System.Collections.Generic;
using System.Linq;

public class FireCircle : Ability
{
    public FireCircle() => Id = "FireCircle";

    public static int CalculateFireCircle(List<AbilityType> abilities)
    {
        int fire = abilities.Where(a => a is AbilityType.FireCircle).Count();

        return fire;
    }
}
