
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public abstract class Enemy : BaseObject, IGameObserver
{
    public Bullet bullet;
    [SerializeField] private GameObject goldCoin;
    public virtual float SightRange() => 2000;
    public virtual float AttackRange() => 2000;

    protected virtual void Start()
    {
        EnemyManager.Instance.AddEnemy(this);
        maxhp *= 1+LevelManager.Instance.CurrentLevel;
        HP = maxhp;
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

    public override void Die(int time = 0)
    {
        EnemyManager.Instance.RemoveEnemy(this);
        //PointManager.Instance.UpDatePoint();

        Player.Instance.ActiveBloodThirst();
        SpawnGoldCoin();
        Destroy(gameObject);
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
        if (collision.gameObject.CompareTag("Wall"))
        {          
            MoveToDirection(RandomDirection(collision.contacts[0].normal));
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(Damage);
        }
        //else if (collision.gameObject.CompareTag("Enemy"))
        //{
        //    // make enemies move through each other
        //    IgnoreCollideWith(collision);
        //}
        else if (collision.gameObject.CompareTag("Bullet"))
        {
            MoveToDirection(Velocity);
        }
    }

    //protected virtual void IgnoreCollideWith(Collision2D collision)
    //{
    //    Physics2D.IgnoreCollision(col, collision.collider, true);
    //    MoveToDirection(Velocity);
    //}

    private void MoveToDirection(Vector2 direction)
    {
        Velocity = direction.normalized * speed;
    }

    private Vector2 RandomDirection(Vector2 n)
    {
        float angle = Vector2.Angle(rb.velocity, n);
        float randomAngle = Random.Range(-angle, angle);

        Vector2 vector = Quaternion.Euler(0, 0, randomAngle) * n;
        return vector;
    }

    public void OnGamePaused(bool isPaused)
    {
        //if (isPaused)
        //{
        //    var scriptComponents = this.GetComponents<MonoBehaviour>();
        //    foreach (var script in scriptComponents)
        //    {
        //        script.enabled = false;
        //    }
        //    velocity = rb.velocity;
        //    rb.velocity = Vector2.zero;
        //}
        //else
        //{
        //    var scriptComponents = this.GetComponents<MonoBehaviour>();
        //    foreach (var script in scriptComponents)
        //    {
        //        script.enabled = true;
        //    }
        //    rb.velocity = velocity;
        //}
    }

    private void SpawnGoldCoin()
    {
        int randomNumber = Random.Range(2, 6);
        for (int i = 0; i < randomNumber; i++)
        {
            GoldCoin gold = GoldCoin.Instantiate(transform.position);
            gold.pointExp = maxhp / (100 * randomNumber);
        }
    }

    #region poisoned blaze freeze
    #region poisoned
    private float poisonedDamage;
    private Coroutine poisonedCoroutine;
    public void Poisoned(float damage)
    {
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
