
using System.Collections;
using UnityEngine;

public class GrimReaperBoss : BossEnemy
{
    const string MOVE = "grim_reaper_move";
    const string DASH = "grim_reaper_dash";
    const string PREPARE_ATK = "grim_reaper_prepare_atk";
    const string ATTACK1 = "grim_reaper_attack1";
    const string ATTACK2 = "grim_reaper_attack2";
    const string ATTACK3 = "grim_reaper_attack3";
    const string DASH_CONDITION = "isDash";

    [SerializeField] Sword sword;
    [SerializeField] bool isAttack;
    [SerializeField] bool isDash;
    [SerializeField] SpriteRenderer sprite;

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (sword.col.enabled) return;
        base.OnCollisionEnter2D(collision);
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

    //public override float Speed 
    //{ 
    //    get
    //    {
    //        if (!isAttack && isDash) return base.Speed + 1;
    //        else return base.Speed;
    //    }
    //    protected set => base.Speed = value;
    //}

    public override void OnInstantiate()
    {
        base.OnInstantiate();
        isAttack = false;
        isDash = true;
        Invoke(nameof(UnableDash), 3);
    }

    public override float AttackRange() => 3;
    public override float SightRange() => 10;

    public override float Patroling()
    {
        enemyAni.Play(MOVE);
        base.Patroling();
        return Random.Range(1f, 2f);
    }

    public override float ChasePlayer(Transform player)
    {
        if (isAttack) return Patroling();
        else if(isDash)
        {
            MoveToDirection(player.position - transform.position);
            return 1;
        }
        else
        {
            isDash = true;
            MoveToDirection(player.position - transform.position);
            StartCoroutine(Dash());
            Invoke(nameof(UnableDash), 4);
            return 0.8f;
        }

    }

    public override float Attack()
    {
        if (isAttack) return Patroling();
        else isAttack = true;

        rb.velocity = Vector2.zero;
        Invoke(nameof(UnableAttack), 3.5f);

        
        StartCoroutine(GrimReaperComboAttack());

        return 1.5f;
    }

    private IEnumerator GrimReaperComboAttack()
    {
        enemyAni.Play(PREPARE_ATK);
        yield return new WaitForSeconds(0.5f);

        sword.StartAttack();
        enemyAni.Play(ATTACK3);
        Vector3 force = Player.Instance.transform.position - transform.position;
        if (force.x < 0) sprite.flipX = true;
        else sprite.flipX = false;
        rb.AddForce(force.normalized * 3);
        yield return new WaitForSeconds(0.25f);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.1f);

        sword.StartAttack();
        enemyAni.Play(ATTACK2);
        //force = Player.Instance.transform.position - transform.position;
        //if (force.x < 0) sprite.flipX = true;
        //else sprite.flipX = false;
        rb.AddForce(force.normalized * 4);
        yield return new WaitForSeconds(0.25f);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.1f);

        sword.StartAttack();
        enemyAni.Play(ATTACK1);
        force = Player.Instance.transform.position - transform.position;
        if (force.x < 0) sprite.flipX = true;
        else sprite.flipX = false;
        rb.AddForce(force.normalized * 5f);
        yield return new WaitForSeconds(0.25f);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.1f);

        sword.EndAttack();
    }


    public IEnumerator Dash()
    {
        sword.StartAttack();

        enemyAni.SetBool(DASH_CONDITION, true);
        yield return new WaitForSeconds(0.1f);
        Velocity = 3 * Speed * (Player.Instance.transform.position - transform.position).normalized;
        yield return new WaitForSeconds(0.6f);

        enemyAni.SetBool(DASH_CONDITION, false);
        yield return new WaitForSeconds(0.1f);

        sword.EndAttack();
    }

    private void UnableAttack()
    {
        isAttack = false;
    }

    private void UnableDash()
    {
        isDash = false;
    }
}
