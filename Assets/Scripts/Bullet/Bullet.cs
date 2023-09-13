


using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Bullet : BaseObject, IGameObserver
{
    public BaseObject owner;
    public Vector3 direction;

    public override void Die(int time = 0)
    {
        GameManager.Instance.UnregisterObserver(this);
        Destroy(gameObject, time);
        if (time != 0) PauseToDie();
    }

    public override void TakeDamage(BaseObject owner) { }

    public override void HittedSound() { }

    protected virtual void Start()
    {      
        GameManager.Instance.RegisterObserver(this);
        BulletCreateSound();
        IgnoreCollideWithOthersBullet();
        IgnoreCollideWithOwn();

        transform.Rotate(Vector3.forward, Vector2.SignedAngle(Vector2.right, direction));

        rb.velocity = direction.normalized * this.speed;
        velocity = rb.velocity;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            BouncyWall bouncy = GetComponent<BouncyWall>();
            if (bouncy != null) bouncy.BulletBounce(collision);
            else Die(1);
        }
    }

    protected virtual void OnCollisionExit2D(Collision2D collision)
    {

    }

    private void IgnoreCollideWithOthersBullet()
    {
        GameManager.Instance.observers.ForEach(observer => {
            if (observer is Bullet b)
            {
                if (b.col != col)
                {
                    Physics2D.IgnoreCollision(col, b.col, true);
                }
            }
        });
    }

    public virtual void BulletCreateSound()
    {
        AudioManager.instance.PlaySound(Sound.Name.BulletCreate2001001.ToString());
    }

    protected virtual void IgnoreCollideWithBullet(Collision2D collision)
    {
        Physics2D.IgnoreCollision(col, collision.collider, true);
        SetVelocity(direction.normalized * Speed);
    }

    private void IgnoreCollideWithOwn()
    {
        if (owner != null)
            Physics2D.IgnoreCollision(col, owner.col, true);
    }

    public void OnGamePaused(bool isPaused)
    {
        gameObject.isStatic = isPaused;

        if (isPaused)
        {
            var scriptComponents = this.GetComponents<MonoBehaviour>();
            foreach (var script in scriptComponents)
            {
                script.enabled = false;
            }
            velocity = rb.velocity;
            rb.velocity = Vector2.zero;
        }
        else
        {
            var scriptComponents = this.GetComponents<MonoBehaviour>();
            foreach (var script in scriptComponents)
            {
                script.enabled = true;
            }
            rb.velocity = velocity;
        }
    }

    private void PauseToDie()
    {
        var scriptComponents = this.GetComponents<MonoBehaviour>();
        scriptComponents.ToList().ForEach(script => Destroy(script));
        Destroy(rb);
        Destroy(col);
    }


    public virtual void ActiveAllAbility()
    {
        Ability[] scriptAbilitys = GetComponents<Ability>();
        List<Ability> abilityList = new List<Ability>();

        foreach (Ability ability in scriptAbilitys)
        {
            if (abilityList.Any(a => a.Name == ability.Name)) continue;
            ability.Active(this);
            abilityList.Add(ability);
        }
    }

    public virtual void RemoveAllAbility()
    {
        Ability[] scriptAbilitys = GetComponents<Ability>();
        foreach (var script in scriptAbilitys)
        {
            Destroy(script);
        }
    }

    public virtual void AddAbility(List<AbilityType> bulletAbilities)
    {
        for (int i = 0; i < bulletAbilities.Count; i++)
        {
            AbilityFactory.AddAbility(bulletAbilities[i], gameObject);
        }
    }

    public static Bullet InstantiateFromOwn(Bullet bullet)
    {
        Transform Owner = bullet.owner.gameObject.transform;
        return Instantiate(bullet, Owner.position, Owner.rotation);
    }
}
