using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy
{
    public override ObjectPoolingType EnemyType => ObjectPoolingType.Bat;

    public List<AbilityType> bulletAbilities;
    int attackRange = 10;

    public override void OnInstantiate()
    {
        base.OnInstantiate();
        attackRange = Random.Range(4, 12);
        GetComponent<EnemyAI>().Oninstantiate(this.transform);
    }

    public override float Patroling()
    {
        attackRange = Random.Range(5, 12);
        base.Patroling();
        return Random.Range(2f, 5f);
    }

    public override float AttackRange() => attackRange;
    public override float SightRange() => 0;

    public override float Attack()
    {
        attackRange = 0;
        rb.velocity = Vector2.zero;
        Invoke(nameof(SpawnBullet), Random.Range(0.5f, 1.2f));
        return Random.Range(1.5f, 2.5f);
    }

    private void SpawnBullet()
    {
        Bullet b = ObjectPooling.Instance
            .GetObject(bullet.BulletType, transform.position).GetComponent<Bullet>();
        b.Direction = GetBulletDirection();
        b.Owner = this;
        b.abilities = new();
        AddAbilityToBullet(b);
        b.ActiveAllAbility();
    }

    private Vector3 GetBulletDirection()
    {
        Vector3 direction = Player.Instance.transform.position - transform.position;

        // t?o góc b?n có ?? l?ch không quá 10
        float randomAngle = Random.Range(-10, 10);
        return Quaternion.Euler(0, 0, randomAngle) * direction;
    }

    private void AddAbilityToBullet(Bullet b)
    {
        if (bulletAbilities.Count > 0)
        {
            List<AbilityType> abi = new();
            int level = LevelManager.Instance.CurrentLevel / 11;
            for (int i = 0; i < level; i++)
            {
                abi.Add(bulletAbilities[Random.Range(0, bulletAbilities.Count)]);
            }
            b.AddAbility(abi);
        }
    }
}
