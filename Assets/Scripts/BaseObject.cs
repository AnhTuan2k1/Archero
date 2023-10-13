

using UnityEngine;
using UnityEngine.UI;

public abstract class BaseObject : MonoBehaviour
{
    public Rigidbody2D rb;
    public Collider2D col;
    [SerializeField] private float hp;
    [SerializeField] protected float maxhp;
    [SerializeField] private float damage;
    [SerializeField] protected float speed;
    [SerializeField] private Vector3 velocity;
    [SerializeField] protected Slider healthBar;
    public float Speed => this.speed;
    public virtual float HP
    {
        get => hp;
        protected set 
        { 
            hp = value;
            UpdateHealth(hp / maxhp);
        }
    }
    public virtual float Damage
    {
        get => damage;
        protected set => damage = value;
    }
    public Vector3 Velocity
    {
        get => velocity;
        set 
        {
            this.velocity = value;
            rb.velocity = velocity;
        }
    }


    public abstract void Die(int time = 0);
    public virtual void HittedSound(DamageType type) { }
    public virtual void TakeDamage(float damage, DamageType type) => HittedSound(type);

    public void UpdateHealth(float fraction)
    {
        if (healthBar != null) healthBar.value = fraction;
    }
}
