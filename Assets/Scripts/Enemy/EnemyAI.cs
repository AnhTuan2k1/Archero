
using System.Collections;
using UnityEngine;


public class EnemyAI : MonoBehaviour
{
    Transform player;
    Transform enemyTransform;
    [SerializeField] Enemy enemy;

    //States
    public bool playerInAttackRange;
    public bool playerInSightRange;

    private void Start()
    {
        enemyTransform = GetComponentInParent<Transform>();
        player = GameObject.FindWithTag("Player")?.transform;

        StartCoroutine(InactiveAfter((float)0.5));
    }


    IEnumerator InactiveAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        CheckPlayerDistance();

        while (true)
        {
            while (playerInAttackRange)
            {
                //print("player In Attack Range");
                //yield return new WaitUntil(() => !GameManager.Instance.IsPaused);

                yield return new WaitForSeconds(enemy.Attack());
                CheckPlayerDistance();
            }

            while (playerInSightRange && !playerInAttackRange)
            {
                //print("player In Sight Range");
                //yield return new WaitUntil(() => !GameManager.Instance.IsPaused);

                yield return new WaitForSeconds(enemy.ChasePlayer(player));
                CheckPlayerDistance();
            }

            while (!playerInSightRange && !playerInAttackRange)
            {
                //print("player So far away");
                // return new WaitUntil(() => !GameManager.Instance.IsPaused);

                yield return new WaitForSeconds(enemy.Patroling());
                CheckPlayerDistance();
            }
        }
    }

    void CheckPlayerDistance()
    {
        float distance = Vector3.Distance(enemyTransform.position, player.position);
        if (distance < enemy.AttackRange())
        {
            playerInAttackRange = true;
            playerInSightRange = true;
        }
        else if (distance < enemy.SightRange())
        {
            playerInAttackRange = false;
            playerInSightRange = true;
        }
        else
        {
            playerInAttackRange = false;
            playerInSightRange = false;
        }
    }
}