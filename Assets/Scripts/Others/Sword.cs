
using UnityEngine;

public class Sword : MonoBehaviour
{
    public Collider2D col;
    [SerializeField] BaseObject owner;
    bool isCausedDamage = false;

    [SerializeField] private float damage;
    public virtual float Damage
    {
        get => damage + owner.Damage;
        protected set => damage = value;
    }

    private void Start()
    {
        col.enabled = false;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isCausedDamage)
        {
            if (collision.gameObject.CompareTag(TagDefine.Tag_Player))
            {
                collision.gameObject.GetComponent<Player>().TakeDamage(Damage);
                isCausedDamage = true;
            }
        }
    }

    public void StartAttack()
    {
        isCausedDamage = false;
    }

    public void EndAttack()
    {
        isCausedDamage = true;
    }
}


// camera limit