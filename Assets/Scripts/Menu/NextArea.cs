using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextArea : MonoBehaviour
{
    [SerializeField] private GameObject blackPanel;
    [SerializeField] private Player player;


    void Start()
    {
        blackPanel.SetActive(false);
        player ??= FindObjectOfType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LevelManager.Instance.IsReadyForNewLevel)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                blackPanel.SetActive(true);
                Invoke(nameof(TurnToNextArea), 1);
            }
        }
    }

    void TurnToNextArea()
    {
        blackPanel?.SetActive(false);

        player?.ReturnToInitialPosition();
        LevelManager.Instance.SpawnNextLevel();
    }

}
