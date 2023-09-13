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
        GameManager.Instance.RegisterObserver(this);
        player = PointManager.Instance.player.transform;

        Random random = new Random();
        int randomNumber = random.Next(-4, 4);
        rb.AddForce(new Vector2(randomNumber, 18) * force);


        Invoke(nameof(Pause), random.Next(6, 9)/10.0f);
    }

    private void Update()
    {
        if (LevelManager.Instance.IsReadyForNewLevel && !isGamepause)
        {
            transform.position = Vector3.MoveTowards(transform
              .position, player.position,
                speed * Time.deltaTime);
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.UnregisterObserver(this);
    }

    private void Pause()
    {
        Destroy(GetComponent<Rigidbody2D>());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(LevelManager.Instance.IsReadyForNewLevel && !isGamepause)
        if (collision.gameObject.CompareTag("Player"))
        {
            PointManager.Instance.GetExpPoint(pointExp);
            Destroy(gameObject);
        }
    }

    public void OnGamePaused(bool isPaused)
    {
        isGamepause = isPaused;
    }
}
