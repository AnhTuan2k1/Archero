
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
        hp = maxhp = 2000 * (1+LevelManager.Instance.CurrentLevel);
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

    public override void TakeDamage(BaseObject owner)
    {
        HittedSound();

        hp -= owner.Damage;
        UpdateHealth(hp / maxhp);

        if (hp <= 0) Die();
        else // push enemy
        {
            Vector2 force = (transform.position - owner.transform.position).normalized * 10;
            rb.AddForce(force);
        }
    }

    public override void Die(int time = 0)
    {
        EnemyManager.Instance.RemoveEnemy(this);
        SpawnGoldCoin();
        Destroy(gameObject);
    }

    public override void HittedSound()
    {
        AudioManager.instance.PlaySound(Sound.Name.Hitted_Body2100001.ToString());
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {          
            MoveToDirection(RandomDirection(collision.contacts[0].normal));
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(this);
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

    protected virtual void IgnoreCollideWith(Collision2D collision)
    {
        Physics2D.IgnoreCollision(col, collision.collider, true);
        MoveToDirection(Velocity);
    }

    private void MoveToDirection(Vector2 direction)
    {
        velocity = direction.normalized * speed;
        rb.velocity = velocity;
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
        if (isPaused)
        {
            var scriptComponents = this.GetComponents<MonoBehaviour>();
            foreach (var script in scriptComponents)
            {
                script.enabled = false;
            }
            velocity = rb.velocity;
            rb.velocity = Vector2.zero;
        }
        else
        {
            var scriptComponents = this.GetComponents<MonoBehaviour>();
            foreach (var script in scriptComponents)
            {
                script.enabled = true;
            }
            rb.velocity = velocity;
        }
    }

    private void SpawnGoldCoin()
    {
        int randomNumber = Random.Range(2, 6);
        for (int i = 0; i < randomNumber; i++)
        {
            GoldCoin gold = GoldCoin.Instantiate(goldCoin, transform.position);
            gold.pointExp = maxhp / (100 * randomNumber);
        }
    }
}
