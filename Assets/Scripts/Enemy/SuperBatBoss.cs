
using System.Collections.Generic;
using UnityEngine;

public class SuperBatBoss : BossEnemy
{
    public List<AbilityType> bulletAbilities;
    int attackRange = 10;
    const string DIE = "SupperBatDie";

    public override void Die(int time = 0)
    {
        enemyAni.Play(DIE);
        base.Die(1000);
    }

    public override float AttackRange() => attackRange;
    public override float SightRange() => 0;

    public override float Patroling()
    {
        attackRange = 10;
        base.Patroling();
        return Random.Range(2f, 4f);
    }

    public override float Attack()
    {
        attackRange = 0;
        rb.velocity = Vector2.zero;
        Invoke(nameof(SpawnBullet), Random.Range(0.5f, 1.2f));
        return Random.Range(2, 3.5f);
    }

    private void SpawnBullet()
    {
        if (GameManager.Instance.IsPaused) return;

        Bullet b = ObjectPooling.Instance
            .GetObject(bullet.BulletType, transform.position).GetComponent<Bullet>();
        b.Direction = GetBulletDirection();
        b.Owner = this;
        b.abilities = new();
        AddAbilityToBullet(b);
        b.ActiveAllAbility();
        b.OnInstantiate();
    }

    private Vector3 GetBulletDirection()
    {
        Vector3 direction = Player.Instance.transform.position - transform.position;

        float randomAngle = Random.Range(-10, 10);
        return Quaternion.Euler(0, 0, randomAngle) * direction;
    }

    private void AddAbilityToBullet(Bullet b)
    {
        if (bulletAbilities.Count > 0)
        {
            List<AbilityType> abi = new() { AbilityType.BouncyWall };
            int level = 4 + (int)(1.2f * LevelManager.Instance.CurrentLevel) / 10;
            for (int i = 0; i < level; i++)
            {
                abi.Add(bulletAbilities[Random.Range(0, bulletAbilities.Count)]);
            }
            b.AddAbility(abi);
        }
    }
}
