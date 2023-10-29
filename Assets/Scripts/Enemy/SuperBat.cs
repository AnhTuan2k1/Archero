using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SuperBat : Enemy
{
    public override ObjectPoolingType EnemyType => ObjectPoolingType.SuperBat;

    [SerializeField] private Animator enemyAni;
    public List<AbilityType> bulletAbilities;
    int attackRange = 10;
    const string DIE = "SupperBatDie";

    public override void OnInstantiate()
    {
        base.OnInstantiate();
        attackRange = Random.Range(4, 12);
    }

    public override void Die(int time = 0)
    {
        enemyAni.Play(DIE);
        rb.velocity = Vector2.zero;
        base.Die(time + 1000);
        //await Task.Delay(1000); if (!isActiveAndEnabled) return;
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
            int level = LevelManager.Instance.CurrentLevel / 11 - 1;
            for (int i = 0; i < level; i++)
            {
                abi.Add(bulletAbilities[Random.Range(0, bulletAbilities.Count)]);
            }
            b.AddAbility(abi);
        }
    }
}
