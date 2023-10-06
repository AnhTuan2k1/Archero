using System.Threading.Tasks;
using UnityEngine;
using Random = System.Random;

public class GoldCoin : MonoBehaviour, IGameObserver
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float force;
    public float pointExp;
    private bool isGamepause = false;

    public Transform player;
    public float speed = 7f;

    void Start()
    {
        OnInstantiate();
    }

    private void Update()
    {
        if (LevelManager.Instance.IsReadyForNewLevel && !isGamepause)
        {
            transform.position = Vector3.MoveTowards(transform
              .position, player.position,
                speed * Time.deltaTime);

            if (Vector2.Distance(player.position, transform.position) < 0.1)
            {
                PointManager.Instance.GetExpPoint(pointExp);
                Die();
            }
        }
    }

    public void Die()
    {
        GameManager.Instance.UnregisterObserver(this);
        ObjectPooling.Instance.ReturnObject(gameObject);
        rb.gravityScale = 1;
    }

    public void OnGamePaused(bool isPaused)
    {
        isGamepause = isPaused;
    }

    public async void OnInstantiate()
    {
        GameManager.Instance.RegisterObserver(this);
        player = Player.Instance.transform;

        Random random = new Random();
        int randomNumber = random.Next(-4, 4);
        rb.AddForce(new Vector2(randomNumber, 18) * force);

        await Task.Delay(random.Next(9, 12) * 100);
        if (gameObject == null) return;
        rb.gravityScale = 0;
        rb.velocity = Vector3.zero;
    }

    public static GoldCoin Instantiate(GameObject goldIcon, Vector3 position)
    {
        GoldCoin g = ObjectPooling.Instance
            .GetObject(goldIcon, position).GetComponent<GoldCoin>();
        g.OnInstantiate();
        return g;
    }
}
