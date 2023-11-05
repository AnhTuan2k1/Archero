

using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseObject : MonoBehaviour, IGameObserver
{
    public Rigidbody2D rb;
    public Collider2D col;
    [SerializeField] protected Slider healthBar;

    [SerializeField] protected float maxhp;
    public virtual float Maxhp
    {
        get => maxhp;
        protected set => maxhp = value;
    }

    [SerializeField] private float speed;
    public virtual float Speed
    {
        get => speed;
        protected set => speed = value;
    }

    [SerializeField] private float hp;
    public virtual float HP
    {
        get => hp;
        protected set 
        { 
            hp = value;
            UpdateHealth(hp / maxhp);
        }
    }

    [SerializeField] private float damage;
    public virtual float Damage
    {
        get => damage;
        protected set => damage = value;
    }

    [SerializeField] private Vector3 velocity;
    public virtual Vector3 Velocity
    {
        get => velocity;
        set 
        {
            this.velocity = value;
            if(rb != null) rb.velocity = velocity;
        }
    }


    public virtual void HittedSound(DamageType type) { }
    public virtual void TakeDamage(float damage, DamageType type) => HittedSound(type);

    public virtual async void Die(int time = 0) 
    {
        await Task.Delay(time);
        GameManager.Instance.UnregisterObserver(this);
    }

    public virtual void OnInstantiate()
    {
        GameManager.Instance.RegisterObserver(this);
    }

    public void UpdateHealth(float fraction)
    {
        if (healthBar != null) healthBar.value = fraction;
    }

    public virtual void OnGamePaused(bool isPaused)
    {
        if (isPaused)
        {
            if (col != null) col.enabled = false;
            if (rb != null) rb.velocity = Vector2.zero;
        }
        else
        {
            if (col != null) col.enabled = true;
            if (rb != null) rb.velocity = Velocity;
        }
    }
}
