using DG.Tweening;
using UnityEngine;

public class FireCircleBullet : Bullet
{
    [SerializeField] GameObject barrier;
    bool isBarrierCollided;
    private void Start()
    {
        owner = Player.Instance;
        isBarrierCollided = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TagDefine.Tag_Enemy))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(Damage, DamageType.Blaze);

            float fireRate = Player.Instance.BlazeRate;
            if (fireRate == 0) fireRate = Blaze.BLAZE_RATE/2f;
            enemy.Burned(Damage * fireRate);

            AudioManager.instance.PlaySound(Sound.Name.FireCol);
        }
        else if (collision.gameObject.CompareTag(TagDefine.Tag_Bullet))
        {
            if (!isBarrierCollided)
            {
                Bullet bullet = collision.gameObject.GetComponent<Bullet>();
                if (bullet.Owner is Enemy)
                {
                    bullet.Die();
                    isBarrierCollided = true;
                    barrier.transform.DOScale(2, 0.1f)
                        .OnComplete(() =>
                        {
                            barrier.transform.localScale = new Vector3(1.2f, 1.2f, 0);
                            barrier.SetActive(false);
                            Invoke(nameof(EnabbleBarrier), 1.9f);
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
