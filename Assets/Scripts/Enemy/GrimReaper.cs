using System.Collections;
using UnityEngine;

public class GrimReaper : Enemy
{
    public override ObjectPoolingType EnemyType => ObjectPoolingType.GrimReaper;


    const string MOVE = "grim_reaper_move";
    const string PREPARE_ATK = "grim_reaper_prepare_atk";
    const string ATTACK1 = "grim_reaper_attack1";
    const string ATTACK2 = "grim_reaper_attack2";
    const string ATTACK3 = "grim_reaper_attack3";

    [SerializeField] Sword sword;
    [SerializeField] protected Animator enemyAni;
    [SerializeField] bool isAttack;
    [SerializeField] bool isPrepareAttacking;
    [SerializeField] SpriteRenderer sprite;

    [SerializeField] Vector2 chasingDirection;
    [SerializeField] bool isChasing;
    public bool IsChasing
    {
        get => isChasing;
        set
        {
            isChasing = value;
            if (!isChasing) chasingDirection = Vector2.zero;
        }
    }

    public override Vector3 Velocity
    {
        get => base.Velocity;
        set
        {
            base.Velocity = value;
            if (value.x < 0) sprite.flipX = true;
            else sprite.flipX = false;
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (isPrepareAttacking || sword.col.enabled)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        else if (IsChasing && (collision.gameObject.CompareTag(TagDefine.Tag_Wall)
            || collision.gameObject.CompareTag(TagDefine.Tag_Water)))
        {
            if (chasingDirection == GetPerpendicular(collision.contacts[0].normal)
                || chasingDirection == -GetPerpendicular(collision.contacts[0].normal))
                MoveToDirection(chasingDirection);
            else
            {
                chasingDirection = GetPerpendicular(collision.contacts[0].normal);
                MoveToDirection(chasingDirection);
            }
            
            return;
        }
        //else if (collision.gameObject.CompareTag(TagDefine.Tag_Bullet))
        //{
        //    MoveToDirection(Velocity);
        //}

        base.OnCollisionEnter2D(collision);
    }

    public override void OnInstantiate()
    {
        isAttack = false;
        IsChasing = false;
        isPrepareAttacking = false;
        base.OnInstantiate();
    }

    public override float AttackRange() => 2;
    public override float SightRange() => 10;

    public override float Patroling()
    {
        IsChasing = false;
        enemyAni.Play(MOVE);
        base.Patroling();
        return Random.Range(2f, 4f);
    }

    public override float ChasePlayer(Transform player)
    {
        IsChasing = true;
        if (isAttack) return Patroling();
        else
        {
            MoveToDirection(player.position - transform.position);
            return 0.5f;
        }

    }

    public override float Attack()
    {
        IsChasing = false;
        if (isAttack) return Patroling();
        else isAttack = true;

        rb.velocity = Vector2.zero;
        Invoke(nameof(UnableAttack), 3.5f);

        StartCoroutine(GrimReaperComboAttack());

        return 1;
    }


    private IEnumerator GrimReaperComboAttack()
    {
        isPrepareAttacking = true;
        Vector3 force = Player.Instance.transform.position - transform.position;
        enemyAni.Play(PREPARE_ATK);
        yield return new WaitForSeconds(0.5f);
        isPrepareAttacking = false;

        sword.StartAttack();

        //Vector3 force = Player.Instance.transform.position - transform.position;
        if (force.x < 0) sprite.flipX = true;
        else sprite.flipX = false;
        Velocity = force.normalized * 7;

        switch (Random.Range(1, 4))
        {
            case 1:
                enemyAni.Play(ATTACK3);
                break;
            case 2:
                enemyAni.Play(ATTACK2);
                break;
            case 3:
                enemyAni.Play(ATTACK1);
                break;
        }

        yield return new WaitForSeconds(0.25f);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.1f);

        sword.EndAttack();
    }

    private void UnableAttack()
    {
        isAttack = false;
    }

    Vector2 GetPerpendicular(Vector3 input)
    {
        
        if (Random.Range(0,2) == 0) return Quaternion.Euler(0, 0, 80) * input;
        else return Quaternion.Euler(0, 0, -80) * input;
    }
}
