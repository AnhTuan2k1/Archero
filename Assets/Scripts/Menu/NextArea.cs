
using UnityEngine;

public class NextArea : MonoBehaviour
{
    [SerializeField] private GameObject blackPanel;
    [SerializeField] private Player player;
    [SerializeField] private Cleaner cleaner;


    void Start()
    {
        blackPanel.SetActive(false);
        player = player != null ? player : FindObjectOfType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LevelManager.Instance.IsReadyForNewLevel)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                blackPanel.SetActive(true);
                cleaner.Clean(transform.position);
                Invoke(nameof(TurnToNextArea), 1);
            }
        }
    }

    void TurnToNextArea()
    {
        blackPanel.SetActive(false);
        player.ReturnToInitialPosition();

        LevelManager.Instance.SpawnEndlessLevel();
    }

}
