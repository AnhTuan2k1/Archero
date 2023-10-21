using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textDieTimes;
    [SerializeField] private int reviveTimes;

    private void Awake()
    {
        reviveTimes = 0;
    }

    public void ShowMenu()
    {
        gameObject.SetActive(true);

        GameManager.Instance.OnGamePaused();
    }

    public void HideMenu()
    {
        gameObject.SetActive(false);

        GameManager.Instance.OnGameResume();
    }

    public void ButtonRevive()
    {
        Player.Instance.Revive();
        reviveTimes++;
        textDieTimes.text = "Die Times: " + reviveTimes;
        HideMenu();
    }

    public void ButtonPlayAgain()
    {
        HideMenu();
        ReloadScene();
    }

    public void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
