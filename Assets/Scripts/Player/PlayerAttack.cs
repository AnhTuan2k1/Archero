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
    public Aim Targeted;

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
            Invoke(nameof(UnableAttack), delayTime/1.1f);
        }
        else if(playerMovement.IsMoving) isReadyAttack = false;
    }

    private void SpawnBullet()
    {
        //Bullet b = Instantiate(bullet, transform.position, transform.rotation);
        Bullet b = ObjectPooling.Instance
            .GetObject(bullet.BulletType, transform.position).GetComponent<Bullet>();
        b.Direction = Targeted.Target - player.transform.position;
        b.Owner = player;
        b.abilities = new();
        b.AddAbility(player.Abilities);
        b.BulletCreateSound();
        b.ActiveAllAbility();
        b.OnInstantiate();
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
}
