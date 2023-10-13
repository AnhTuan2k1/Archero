using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Player player;
    private PlayerMovement playerMovement;
    [SerializeField] float delayTime = 1;
    [SerializeField] bool isAttacked = false;
    [SerializeField] bool isReadyAttack = false;
    public Bullet bullet;

    public float AttackSpeed
    {
        get { return delayTime; }
        set { delayTime = value; }
    }

    void Start()
    {
        player = GetComponent<Player>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        ArrowAttack();
    }

    public void ArrowAttack()
    {
        if (!isAttacked && !playerMovement.IsMoving && isReadyAttack)
        {
            if (EnemyManager.Instance.Enemies.Count == 0) return;

            isAttacked = true;
            SpawnBullet();
            Invoke(nameof(UnableAttack), delayTime);
        }
        else if(playerMovement.IsMoving) isReadyAttack = false;
    }

    private void SpawnBullet()
    {
        //Bullet b = Instantiate(bullet, transform.position, transform.rotation);
        Bullet b = ObjectPooling.Instance
            .GetObject(bullet.BulletType, transform.position).GetComponent<Bullet>();
        b.Direction = enemyPosition() - player.transform.position;
        b.Owner = player;
        b.abilities = new();
        b.AddAbility(player.Abilities);
        b.BulletCreateSound();
        b.ActiveAllAbility();
    }

    private void UnableAttack()
    {
        isAttacked = false;
    }

    public void InvokeUnableReadyAttack()
    {
        Invoke(nameof(UnableReadyAttack), delayTime/(float)0.9);
    }
    private void UnableReadyAttack()
    {
        isReadyAttack = true;
    }

    private Vector3 enemyPosition()
    {
        try
        {
            Vector3 position = Vector3.zero;
            if (EnemyManager.Instance.Enemies.Count == 0) return position;
            else
            {
                List<Enemy> enemys = EnemyManager.Instance.Enemies;
                position = enemys[0].transform.position;
                float distance = Vector2.Distance(enemys[0].transform.position, player.transform.position);
                for (int i = 1; i < enemys.Count; i++)
                {
                    float d = Vector2.Distance(enemys[i]
                        .transform.position, player.transform.position);
                    if (d < distance)
                    {
                        distance = d;
                        position = enemys[i].transform.position;
                    }
                }
                return position;
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning(e);
            EnemyManager.Instance.Enemies.RemoveAll(x => !x);
            return Vector3.zero;
        }
    }
}
