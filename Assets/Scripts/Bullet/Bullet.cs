﻿

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public abstract class Bullet : BaseObject
{
    public List<Ability> abilities;
    public virtual ObjectPoolingType BulletType => ObjectPoolingType.None;

    public override float Damage 
    { 
        get => base.Damage + Owner.Damage; 
        protected set => base.Damage = value; 
    }

    [SerializeField] protected Vector3 direction;
    public Vector3 Direction 
    { 
        get => direction;
        set
        {
            transform.Rotate(Vector3.forward, Vector2.SignedAngle(direction, value));
            direction = value.normalized;
            Velocity = direction * Speed;
        }
    }

    [SerializeField] protected BaseObject owner;
    public BaseObject Owner 
    { 
        get => owner; 
        set
        {
            owner = value;
            if(col != null) Physics2D.IgnoreCollision(col, owner.col, true);
        } 
    }

    public async override void Die(int time = 0)
    {
        col.enabled = false;
        rb.velocity = Vector2.zero;
        await Task.Delay(time);
        if (this == null) return;
        col.enabled = true;

        ObjectPooling.Instance.ReturnObject(gameObject, BulletType);
        base.Die(time);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Die();
        }
    }

    protected virtual void OnCollisionExit2D(Collision2D collision)
    {

    }

    public virtual void BulletCreateSound()
    {
        AudioManager.instance.PlaySound(Sound.Name.BulletCreate2001001);
    }

    public virtual void ActiveAllAbility()
    {
        List<Ability> abilityList = new();

        foreach (Ability ability in abilities)
        {
            if (abilityList.Any(a => a.Id == ability.Id)) continue;
            ability.Active(this);
            abilityList.Add(ability);
        }
    }

    public virtual void AddAbility(List<AbilityType> bulletAbilities)
    {
        for (int i = 0; i < bulletAbilities.Count; i++)
        {
            AbilityFactory.AddAbility(bulletAbilities[i], this.abilities);
        }
    }

    public virtual void AddAbility(List<Ability> bulletAbilities)
    {
        for (int i = 0; i < bulletAbilities.Count; i++)
        {
            AbilityFactory.AddAbility(bulletAbilities[i], this.abilities);
        }
    }

    public static Bullet InstantiateFromOwn(Bullet bullet)
    {
        if (bullet.owner == null) return null;
        Transform Owner = bullet.owner.gameObject.transform;

        Bullet b = ObjectPooling.Instance
            .GetObject(bullet.BulletType, Owner.transform.position).GetComponent<Bullet>();

        b.Owner = bullet.Owner;
        b.Direction = bullet.Direction;
        b.abilities = new();
        b.AddAbility(bullet.abilities);
        //b.BulletCreateSound();
        b.OnInstantiate();
        return b;
    }
}
