using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class GoldCoin : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D col;
    [SerializeField] private float force;
    public float pointExp;

    private Transform player;
    private float speed = 8;

    IEnumerator MovetoPlayer()
    {
        while (true)
        {
            while (LevelManager.Instance.IsReadyForNewLevel)
            {
                if (GameManager.Instance.IsPaused) rb.velocity = Vector2.zero;
                else if(Vector2.Distance(player.position, transform.position) < 0.1f)
                {
                    PointManager.Instance.GetExpPoint(pointExp);
                    Die();
                }
                else rb.velocity = (player.position - transform.position).normalized * speed;

                yield return new WaitForEndOfFrame();
            }

            if (rb.velocity != Vector2.zero) rb.velocity = Vector2.zero;
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
        if (!isActiveAndEnabled) return;
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
