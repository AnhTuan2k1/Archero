
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public abstract class Enemy : BaseObject
{
    public Bullet bullet;
    [SerializeField] protected int initalMaxHealth;
    [SerializeField] protected int initalDamage;
    public virtual ObjectPoolingType EnemyType => ObjectPoolingType.None;
    public virtual float SightRange() => 2000;
    public virtual float AttackRange() => 2000;
    [SerializeField] bool CausedDamage = false;

    public static Enemy Instantiate(Vector3 position, ObjectPoolingType type)
    {
        Enemy enemy = ObjectPooling.Instance
            .GetObject(type).GetComponent<Enemy>();
        enemy.transform.position = position;

        enemy.OnInstantiate();
        return enemy;
    }

    public virtual void OnInstantiate()
    {
        col.enabled = true;
        EnemyManager.Instance.AddEnemy(this);

        int level = LevelManager.Instance.CurrentLevel;
        maxhp = initalMaxHealth * (1 + 5*(level / 10) + 0.2f * (level % 10))
            + Mathf.Pow(2, 3 + level / 10) * 10;
        HP = maxhp;
        Damage = initalDamage * (1 + 0.5f * level / 10 + 0.02f * (level % 10));
    }

    public virtual float Patroling()
    {
        MoveToDirection(Random.insideUnitSphere);
        return 10;
    }
    public virtual float ChasePlayer(Transform player)
    {
        MoveToDirection(player.position - transform.position);
        return 10;
    }
    public virtual float Attack()
    {
        rb.velocity = Vector2.zero;    // stop move
        Debug.Log(gameObject.name + ": Attack");
        return 10;
    }

    public override void TakeDamage(float damage, DamageType type = DamageType.Nomal)
    {
        base.TakeDamage(damage, type);

        FloatingText.Instantiate(transform.position)
            .SetText(((int)damage).ToString(), type);

        HP -= damage;
        if (HP <= 0) Die();
    }

    public override async void Die(int time = 0)
    {
        col.enabled = false;

        EnemyManager.Instance.RemoveEnemy(this);
        PointManager.Instance.AddPoint(1);

        Player.Instance.ActiveBloodThirst();
        SpawnGoldCoin();

        await Task.Delay(100);
        ObjectPooling.Instance.ReturnObject(gameObject);
    }

    public override void HittedSound(DamageType type)
    {
        switch (type)
        {
            case DamageType.Nomal:
                AudioManager.instance.PlaySound(Sound.Name.Hitted_Body2100001);
                break;
            case DamageType.Bolt:
                AudioManager.instance.PlaySound(Sound.Name.Thunder);
                break;
        }

    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(TagDefine.Tag_Wall))
        {          
            MoveToDirection(RandomDirection(collision.contacts[0].normal));
        }
        else if (collision.gameObject.CompareTag(TagDefine.Tag_Player))
        {
            if (CausedDamage) return;

            collision.gameObject.GetComponent<Player>().TakeDamage(Damage);
            CausedDamage = true;
            EnableCausedDamageAgain(800);
        }
        else if (collision.gameObject.CompareTag(TagDefine.Tag_Bullet))
        {
            MoveToDirection(Velocity);
        }
    }

    private async void EnableCausedDamageAgain(int time)
    {
        await Task.Delay(time);
        if (this == null) return;
        CausedDamage = false;
    }

    private void MoveToDirection(Vector2 direction)
    {
        Velocity = direction.normalized * Speed;
    }

    private Vector2 RandomDirection(Vector2 n)
    {
        float angle = Vector2.Angle(rb.velocity, n);
        float randomAngle = Random.Range(-angle, angle);

        Vector2 vector = Quaternion.Euler(0, 0, randomAngle) * n;
        return vector;
    }

    private void SpawnGoldCoin()
    {
        int randomNumber = Random.Range(2, 6);
        for (int i = 0; i < randomNumber; i++)
        {
            GoldCoin gold = GoldCoin.Instantiate(transform.position);
            gold.pointExp = 14.0f / randomNumber;
        }
    }

    #region poisoned blaze freeze
    #region poisoned
    private float poisonedDamage;
    private Coroutine poisonedCoroutine;
    public void Poisoned(float damage)
    {
        if (!isActiveAndEnabled) return;
        if (poisonedDamage != damage) poisonedDamage = damage;
        poisonedCoroutine ??= StartCoroutine(StartPoisoned());
    }

    private IEnumerator StartPoisoned()
    {
        while (true)
        {
            yield return new WaitForSeconds(PoisonedTouch.POISONED_DELAY);
            TakeDamage(poisonedDamage, DamageType.Poisoned);
        }
    }

    #endregion
    #region blaze
    private float burnDamage;
    private float burnTime;
    public void Burned(float damage)
    {
        if (!isActiveAndEnabled) return;
        if (burnDamage != damage) burnDamage = damage;

        if (burnTime > 0)
        {
            burnTime = Blaze.BLAZE_TIME;
        }
        else
        {
            burnTime = Blaze.BLAZE_TIME;
            StartCoroutine(StartBurn());
        }

    }

    private IEnumerator StartBurn()
    {
        while (burnTime > 0)
        {
            yield return new WaitForSeconds(Blaze.BLAZE_DELAY);
            burnTime -= Blaze.BLAZE_DELAY;
            TakeDamage(burnDamage, DamageType.Blaze);
        }
    }
    #endregion

    #endregion
}
