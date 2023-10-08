


using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Bullet : BaseObject, IGameObserver
{
    protected BaseObject owner;
    protected Vector3 direction;
    public List<Ability> abilities;

    public Vector3 Direction 
    { 
        get => direction;
        set
        {
            transform.Rotate(Vector3.forward, Vector2.SignedAngle(direction, value));
            direction = value.normalized;
            Velocity = direction * speed;
        }
    }

    public BaseObject Owner 
    { 
        get => owner; 
        set
        {
            owner = value;
            Physics2D.IgnoreCollision(col, owner.col, true);
        } 
    }

    public override void Die(int time = 0)
    {
        GameManager.Instance.UnregisterObserver(this);
        ObjectPooling.Instance.ReturnObject(gameObject, time);
    }

    public override void TakeDamage(BaseObject owner) { }
    public override void HittedSound() { }

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
        AudioManager.instance.PlaySound(Sound.Name.BulletCreate2001001.ToString());
    }

    public void OnGamePaused(bool isPaused)
    {
        //gameObject.isStatic = isPaused;

        //if (isPaused)
        //{
        //    var scriptComponents = this.GetComponents<MonoBehaviour>();
        //    foreach (var script in scriptComponents)
        //    {
        //        script.enabled = false;
        //    }
        //    velocity = rb.velocity;
        //    rb.velocity = Vector2.zero;
        //}
        //else
        //{
        //    var scriptComponents = this.GetComponents<MonoBehaviour>();
        //    foreach (var script in scriptComponents)
        //    {
        //        script.enabled = true;
        //    }
        //    rb.velocity = velocity;
        //}
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

        //Bullet b = Instantiate(bullet, Owner.position, Owner.rotation);
        Bullet b = ObjectPooling.Instance
            .GetObject(bullet.gameObject, Owner.transform.position).GetComponent<Bullet>();
        GameManager.Instance.RegisterObserver(b);

        b.Owner = bullet.Owner;
        b.Direction = bullet.Direction;
        b.abilities = new();
        b.AddAbility(bullet.abilities);
        //b.BulletCreateSound();
        return b;
    }

}
