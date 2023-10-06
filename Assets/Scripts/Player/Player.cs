
using System;
using TMPro;
using UnityEngine;

public class Player : BaseObject, IGameObserver
{
    public TextMeshProUGUI textPoint;
    public float maxHealth = 1000;
    public PlayerAttack playerAttack;

    private static Player _instance;
    public static Player Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<Player>();
            return _instance;
        }
    }

    protected virtual void Start()
    {
        GameManager.Instance.RegisterObserver(this);
    }

    public override void Die(int time = 0)
    {
        GameManager.Instance.UnregisterObserver(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // prevent enemy push player temporary.
            Physics2D.IgnoreCollision(col, collision.collider, true);
            rb.velocity = Vector3.zero;
        }
        else 
        if (collision.gameObject.CompareTag("Bullet"))
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Physics2D.IgnoreCollision(col, collision.collider, false);
        }
    }

    public override void HittedSound()
    {
        AudioManager.instance.PlaySound(Sound.Name.BodyHit4100001.ToString());
    }

    public override void TakeDamage(BaseObject owner)
    {
        HittedSound();

        hp = hp - owner.Damage;
        UpdateHealth(hp / maxHealth);
        textPoint.SetText(((int)hp).ToString());
        if (hp <= 0) Die();
    }

    public void OnGamePaused(bool isPaused)
    {
        if (isPaused)
        {
            var scriptComponents = this.GetComponents<MonoBehaviour>();
            foreach (var script in scriptComponents)
            {
                script.enabled = false;
            }
        }
        else
        {
            var scriptComponents = this.GetComponents<MonoBehaviour>();
            foreach (var script in scriptComponents)
            {
                script.enabled = true;
            }
        }
    }

    public void ReturnToInitialPosition()
    {
        gameObject.transform.position = new Vector2(0, -2);
    }

    public void AddAbility(AbilityType type)
    {
        //playerAttack.bullet.gameObject.AddComponent<>();
        //AbilityFactory.AddAbility(type, playerAttack.bullet.gameObject);
        playerAttack.abilities.Add(type);
    }
}
