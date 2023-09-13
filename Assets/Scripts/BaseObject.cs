

using UnityEngine;
using UnityEngine.UI;

public abstract class BaseObject : MonoBehaviour
{
    public Rigidbody2D rb;
    public Collider2D col;
    [SerializeField] protected float hp;
    [SerializeField] protected float maxhp;
    [SerializeField] protected float damage;
    [SerializeField] protected float speed;
    [SerializeField] protected Vector3 velocity;
    [SerializeField] protected Slider healthBar;
    public float Hp => this.hp;
    public float Damage => this.damage;
    public float Speed => this.speed;
    public Vector3 Velocity => this.velocity;

    public abstract void Die(int time = 0);
    public abstract void TakeDamage(BaseObject owner);
    public abstract void HittedSound();

    public void UpdateHealth(float fraction)
    {
        if (healthBar != null) healthBar.value = fraction;
    }
    public void SetVelocity(Vector3 velocity)
    {
        rb.velocity = velocity;
        this.velocity = velocity;
    }
}
