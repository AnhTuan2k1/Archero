

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bolt : Ability
{
    public static readonly int RANGE_ACTIVITY = 4;
    public static readonly float BOLT_RATE = 0.3f;
    public Bolt() => Id = "Bolt";

    public static float CalculateBoltRate(List<AbilityType> abilities)
    {
        int bolt = abilities.Where(a => a is AbilityType.Bolt).Count();
        float rate = bolt * BOLT_RATE;

        return rate;
    }

    public static void LightningStrikeEnemies(Vector3 origin, float damage)
    {
        try
        {
            foreach (Enemy e in EnemyManager.Instance.Enemies)
            {
                Vector3 position = e.gameObject.transform.position;
                float distance = Vector2.Distance(position, origin);
                if (0.1 < distance && distance < RANGE_ACTIVITY)
                {
                    LightningBolt.Instantiate(origin, position);
                    e.TakeDamage(damage, DamageType.Bolt);
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e);
            EnemyManager.Instance.Enemies.RemoveAll(x => !x);
        }
    }

}
