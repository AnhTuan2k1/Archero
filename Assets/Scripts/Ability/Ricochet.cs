

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ricochet : Ability
{
    public Ricochet() => Name = "Ricochet";
    public static readonly int RANGE_ACTIVITY = 4;
    [SerializeField] private int ricochetTimes = 3;

    /// <summary>
    /// true if Ricochet actived, otherwise false
    /// </summary>
    public bool ActiveRicochet(Vector3 enemy)
    {
        if (ricochetTimes > 0)
        {
            float distance = 1000;
            Vector3 target = enemyPosition(ref distance, enemy);
            if (distance < RANGE_ACTIVITY)
            {
                Bullet bullet = GetComponent<Bullet>();
                Vector2 newDirection = target - transform.position;

                bullet.transform.Rotate(Vector3.forward, Vector2.SignedAngle(bullet.direction, newDirection));
                bullet.direction = newDirection;
                bullet.SetVelocity(newDirection.normalized * bullet.Speed);

                ricochetTimes--;
                if (ricochetTimes < 1) Destroy(GetComponent<Ricochet>());
                return true;
            }
            else return false;
        }
        else return false;
    }

    private Vector3 enemyPosition(ref float distance, Vector3 enemy)
    {
        List<Enemy> enemys = EnemyManager.Instance.Enemies;

        if (enemys.Count < 2) return Vector3.zero;
        enemys = enemys.OrderBy(e => Vector3.Distance(e.transform.position, enemy)).ToList();
        distance = Vector2.Distance(enemys[1].transform.position, enemy);
        
        return enemys[1].transform.position;
    }
}