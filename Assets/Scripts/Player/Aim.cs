
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    [SerializeField] private Enemy target;

    private void Update()
    {
        if(target != null && target.isActiveAndEnabled)
        {
            transform.position = target.transform.position;
        }
        else transform.position = Vector3.zero;
    }

    public Vector3 Target
    {
        get 
        {
            if (target == null || !target.isActiveAndEnabled) FindCloestTaget();

            if(target == null || !target.isActiveAndEnabled)
            {
                return EnemyManager.Instance.Clean();
            }
            else return target.transform.position; 
        }
    }

    public Enemy FindCloestTaget()
    {
        List<Enemy> enemys = EnemyManager.Instance.Enemies;
        Transform player = Player.Instance.transform;

        if (enemys.Count == 0) target = null;
        else if (enemys.Count == 1) target = enemys[0];
        else
        {
            target = enemys[0];
            float distance = Vector2.Distance(target.transform.position, player.position);
            
            for (int i = 1; i < enemys.Count; i++)
            {
                float d = Vector2.Distance(enemys[i]
                    .transform.position, player.position);
                if (d < distance)
                {
                    distance = d;
                    target = enemys[i];
                }
            }

        }

        return target;
    }

    public Enemy ChangeTarget()
    {
        List<Enemy> enemys = EnemyManager.Instance.Enemies;
        Transform player = Player.Instance.transform;

        if (enemys.Count == 0) target = null;
        else if(enemys.Count == 1 || target == null) target = enemys[0];
        else if(target != null || target.isActiveAndEnabled) // already had target
        {
            float newDistance = 1000;
            Enemy oldTarget = target;
            foreach (Enemy enemy in enemys)
            {
                if(enemy != oldTarget)
                {
                    float d = Vector2.Distance(enemy.transform.position, player.position);
                    if (d < newDistance)
                    {
                        newDistance = d;
                        target = enemy;
                    }
                }

            }
        }
        else //no target
        {
            target = enemys[0];
            float distance = Vector2.Distance(target.transform.position, player.position);

            for (int i = 1; i < enemys.Count; i++)
            {
                float d = Vector2.Distance(enemys[i]
                    .transform.position, player.position);
                if (d < distance)
                {
                    distance = d;
                    target = enemys[i];
                }
            }

        }

        return target;
    }
}
