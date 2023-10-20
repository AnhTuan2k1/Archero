

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ricochet : Ability
{
    public Ricochet() => Id = "Ricochet";
    public static readonly int RANGE_ACTIVITY = 3;
    [SerializeField] private int ricochetTimes = 3;

    /// <summary>
    /// return true if Ricochet actived, otherwise false
    /// </summary>
    public bool ActiveRicochet(Vector3 enemy, Bullet bullet)
    {
        if (ricochetTimes > 0)
        {
            float distance = 1000;
            Vector3 target = EnemyPosition(ref distance, enemy);
            if (distance < RANGE_ACTIVITY)
            {
                Vector2 newDirection = target - bullet.transform.position;
                bullet.Direction = newDirection;

                ricochetTimes--;
                if (ricochetTimes < 1) bullet.abilities.Remove(this);
                return true;
            }
            else return false;
        }
        else return false;
    }

    private Vector3 EnemyPosition(ref float distance, Vector3 enemy)
    {
        List<Enemy> enemys = EnemyManager.Instance.Enemies;

        if (enemys.Count < 2) return Vector3.zero;
        enemys = enemys.OrderBy(e => Vector3.Distance(e.transform.position, enemy)).ToList();
        distance = Vector2.Distance(enemys[1].transform.position, enemy);
        
        return enemys[1].transform.position;
    }
}
