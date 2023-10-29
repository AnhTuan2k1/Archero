using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SuperBatBoss : BossEnemy, IGameObserver
{
    [SerializeField] private Animator enemyAni;
    public List<AbilityType> bulletAbilities;
    int attackRange = 10;
    const string DIE = "SupperBatDie";

    private void Start()
    {
        OnInstantiate();
    }

    public override void OnInstantiate()
    {
        int level = LevelManager.Instance.CurrentLevel;
        maxhp = initalMaxHealth * (1 + 3 * level / 10)
            + 5000 + Mathf.Pow(2, 4 + level / 10) * 10;
        HP = maxhp;
        Damage = initalDamage;

        burnTime = 0;
        poisonedCoroutine = null;

        attackRange = Random.Range(4, 12);
        GetComponent<EnemyAI>().Oninstantiate(this.transform);

        EnemyManager.Instance.AddEnemy(this);
        GameManager.Instance.expBar.SetActive(false);
        GameManager.Instance.RegisterObserver(this);
    }

    public override async void Die(int time = 0)
    {
        GameManager.Instance.expBar.SetActive(true);
        GameManager.Instance.UnregisterObserver(this);
        PointManager.Instance.AddPoint(25);
        Player.Instance.ActiveBloodThirst();

        StopAllCoroutines();
        GetComponent<EnemyAI>().StopAllCoroutines();
        col.enabled = false;
        EnemyManager.Instance.RemoveEnemy(this);

        enemyAni.Play(DIE);
        await Task.Delay(1000); if (this == null) return;
        Destroy(gameObject);

        AbilityManager.Instance.ShowAbilityMenu();
    }

    public override float Patroling()
    {
        attackRange = Random.Range(5, 12);
        base.Patroling();
        return Random.Range(2f, 3f);
    }

    public override float AttackRange() => attackRange;
    public override float SightRange() => 0;

    public override float Attack()
    {
        attackRange = 0;
        rb.velocity = Vector2.zero;
        Invoke(nameof(SpawnBullet), Random.Range(0.5f, 1.2f));
        return Random.Range(2, 3.5f);
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
            List<AbilityType> abi = new(){ AbilityType.BouncyWall };
            int level = 4 + (int)(1.5f * LevelManager.Instance.CurrentLevel) / 10;
            for (int i = 0; i < level; i++)
            {
                abi.Add(bulletAbilities[Random.Range(0, bulletAbilities.Count)]);
            }
            b.AddAbility(abi);
        }
    }
}
