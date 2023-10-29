using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textDieTimes;
    [SerializeField] private int reviveTimes;
    [SerializeField] private GameObject buttonRevive;

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
        HideMenu();
        Player.Instance.Revive();
        reviveTimes--;
        textDieTimes.text = "Lives: " + reviveTimes;

        if(reviveTimes <= 0) buttonRevive.SetActive(false);
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
