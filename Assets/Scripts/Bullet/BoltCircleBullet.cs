using DG.Tweening;
using UnityEngine;

public class BoltCircleBullet : Bullet
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
            enemy.TakeDamage(Damage, DamageType.Bolt);

            if (!enemy.isActiveAndEnabled) return;

            float boltRate = Player.Instance.BoltRate;
            if (boltRate == 0) boltRate = Bolt.BOLT_RATE/2 ;
            Bolt.LightningStrikeEnemies(enemy.transform.position, Damage * boltRate);
        }
        else if (collision.gameObject.CompareTag(TagDefine.Tag_Bullet))
        {
            if (!isBarrierCollided)
            {
                Bullet bullet = collision.gameObject.GetComponent<Bullet>();
                if (bullet.Owner is Enemy)
                {
                    bullet.Die();
                    isBarrierCollided=true;
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
