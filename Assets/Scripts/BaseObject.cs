

using UnityEngine;
using UnityEngine.UI;

public abstract class BaseObject : MonoBehaviour
{
    public Rigidbody2D rb;
    public Collider2D col;
    [SerializeField] private float hp;
    [SerializeField] protected float maxhp;
    [SerializeField] protected float damage;
    [SerializeField] protected float speed;
    [SerializeField] private Vector3 velocity;
    [SerializeField] protected Slider healthBar;
    [SerializeField] protected GameObject floatingTextPrefab;
    public virtual float Damage => this.damage;
    public float Speed => this.speed;
    public virtual float HP
    {
        get { return hp; }
        protected set 
        { 
            hp = value;
            UpdateHealth(hp / maxhp);
        }
    }
    public Vector3 Velocity
    {
        get { return velocity; }
        set 
        {
            this.velocity = value;
            rb.velocity = velocity;
        }
    }


    public abstract void Die(int time = 0);
    public abstract void HittedSound();
    public virtual void TakeDamage(BaseObject owner) => HittedSound();

    public void UpdateHealth(float fraction)
    {
        if (healthBar != null) healthBar.value = fraction;
    }
}
