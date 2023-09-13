


using System.Collections.Generic;
using UnityEngine;

public class Enemy2001 : Enemy
{
    public List<AbilityType> bulletAbilities;
    int attackRange = 10;

    public override float Patroling()
    {
        attackRange = Random.Range(5, 12);
        base.Patroling();
        return Random.Range(2f, 5f);
    }

    public override float AttackRange() => attackRange;
    public override float SightRange() => 0;

    protected override void Start()
    {
        base.Start();
        attackRange = Random.Range(4, 12);
    }


    public override float Attack()
    {
        attackRange = 0;
        rb.velocity = Vector2.zero;
        Invoke(nameof(SpawnBullet), Random.Range(0.5f, 1.2f));
        return Random.Range(1.5f, 2.5f);
    }

    private void SpawnBullet()
    {
        Bullet b = Instantiate(bullet, transform.position, transform.rotation);
        b.direction = GetBulletDirection();
        b.owner = this;
        AddAbilityToBullet(b);
        b.ActiveAllAbility();
    }

    private Vector3 GetBulletDirection()
    {
        Vector3 direction = Player.Instance.transform.position - transform.position;
        
        // tạo góc bắn có độ lệch lớn nhất là 10
        float randomAngle = Random.Range(-10, 10);
        return Quaternion.Euler(0, 0, randomAngle) * direction;
    }

    private void AddAbilityToBullet(Bullet b)
    {
        List<AbilityType> abi = new();      
        for (int i = 0; i < Random.Range(0, 5); i++)
        {
            abi.Add(bulletAbilities[Random.Range(0, 4)]);
        }
        b.AddAbility(abi);
    }
}
