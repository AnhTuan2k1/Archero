

using System.Threading.Tasks;
using UnityEngine;

public abstract class BossEnemy : Enemy, IGameObserver
{
    [SerializeField] protected Animator enemyAni;

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
        rb.velocity = Vector2.zero;
        EnemyManager.Instance.RemoveEnemy(this);

        await Task.Delay(time); if (this == null) return;
        Destroy(gameObject);

        AbilityManager.Instance.ShowAbilityMenu();
    }

    public override float Patroling()
    {
        base.Patroling();
        return Random.Range(2f, 3f);
    }
}
