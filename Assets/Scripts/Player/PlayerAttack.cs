using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Player player;
    private PlayerMovement playerMovement;
    [SerializeField] float delayTime = 1.0f;
    [SerializeField] bool isAttacked = false;
    [SerializeField] bool isReadyAttack = false;
    //[SerializeField] bool isMouseButtonDown = true;
    public Bullet bullet;
    public List<AbilityType> bulletAbilities;
    [SerializeField] Vector3 mouseUpPosition;

    void Start()
    {
        player = GetComponent<Player>();
        playerMovement = GetComponent<PlayerMovement>();
        bullet.RemoveAllAbility();
        bulletAbilities ??= new List<AbilityType>();
    }

    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    mouseUpPosition = Camera.main.ScreenToWorldPoint(
        //        new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        //    mouseUpPosition = new Vector3(mouseUpPosition.x, mouseUpPosition.y, 0);

        //    isMouseButtonDown = true;
        //}
        //if (Input.GetMouseButtonUp(0))
        //{
        //    Invoke(nameof(UnableMouseButtonDown), delayTime/2);
        //}

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
        Bullet b = Instantiate(bullet, transform.position, transform.rotation);
        b.direction = enemyPosition() - player.transform.position;
        b.owner = player;
        b.AddAbility(bulletAbilities);
        b.ActiveAllAbility();
    }

    private void UnableAttack()
    {
        isAttacked = false;
    }

    public void InvokeUnableReadyAttack()
    {
        Invoke(nameof(UnableReadyAttack), delayTime/(float)1.5);
    }
    private void UnableReadyAttack()
    {
        isReadyAttack = true;
    }
    //private void UnableMouseButtonDown()
    //{
    //    isMouseButtonDown = false;
    //}

    private Vector3 enemyPosition()
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

}
