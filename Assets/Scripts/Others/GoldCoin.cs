using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class GoldCoin : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D col;
    [SerializeField] private float force;
    public float pointExp;

    public Transform player;
    public float speed = 7f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LevelManager.Instance.IsReadyForNewLevel)
        {
            if (collision.gameObject.CompareTag(TagDefine.Tag_Player))
            {
                PointManager.Instance.GetExpPoint(pointExp);
                Die();
            }
        }
    }

    IEnumerator MovetoPlayer()
    {
        while (true)
        {
            while (LevelManager.Instance.IsReadyForNewLevel)
            {
                if (GameManager.Instance.IsPaused)
                    rb.velocity = Vector2.zero;
                else
                    rb.velocity = (player.position - transform.position).normalized * speed;

                yield return new WaitForEndOfFrame();
            }

            
            yield return new WaitForEndOfFrame();
        }

    }

    public void Die()
    {
        StopAllCoroutines();
        rb.gravityScale = 1;

        ObjectPooling.Instance.ReturnObject(gameObject);
    }

    public async void OnInstantiate()
    {
        player = Player.Instance.transform;

        float randomNumber = Random.Range(-4, 4.0f);
        rb.AddForce(new Vector2(randomNumber, 18) * force);

        await Task.Delay((int)(Random.Range(5.0f, 8) * 100));
        if (this == null) return;
        rb.gravityScale = 0;
        rb.velocity = Vector3.zero;
        StartCoroutine(MovetoPlayer());
    }

    public static GoldCoin Instantiate(Vector3 position)
    {
        GoldCoin g = ObjectPooling.Instance
            .GetObject(ObjectPoolingType.GoldCoin, position).GetComponent<GoldCoin>();
        g.OnInstantiate();
        return g;
    }
}
