

using DG.Tweening;
using UnityEngine;

public static class TagDefine
{
    public static readonly string Tag_Player = "Player";
    public static readonly string Tag_Enemy = "Enemy";
    public static readonly string Tag_Bullet = "Bullet";
    public static readonly string Tag_Wall = "Wall";
}

public abstract class CircleBullet : Bullet
{
    [SerializeField] GameObject barrier;
    bool isBarrierCollided;
    protected virtual void Start()
    {
        owner = Player.Instance;
        isBarrierCollided = false;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TagDefine.Tag_Bullet))
        {
            if (!isBarrierCollided)
            {
                Bullet bullet = collision.gameObject.GetComponent<Bullet>();
                if (bullet.Owner is Enemy)
                {
                    bullet.Die();
                    isBarrierCollided = true;
                    barrier.transform.DOScale(2, 0.2f)
                        .OnComplete(() =>
                        {
                            barrier.transform.localScale = new Vector3(1.2f, 1.2f, 0);
                            barrier.SetActive(false);
                            Invoke(nameof(EnabbleBarrier), 1.8f);
                        });
                }
            }
        }
    }

    private void EnabbleBarrier()
    {
        if (GameManager.Instance.IsPaused)
        {
            Invoke(nameof(EnabbleBarrier), 1.9f);
            return;
        }
        else
        {
            barrier.SetActive(true);
            isBarrierCollided = false;
        }
    }
}
