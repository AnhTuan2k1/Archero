using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public GameObject expBar;
    public GameOverMenu gameOverMenu;

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<GameManager>();
            return _instance;
        }
    }

    public List<IGameObserver> observers = new List<IGameObserver>();
    [SerializeField] private bool isPaused = false;
    public bool IsPaused 
    { 
        get => isPaused;
        set
        {
            isPaused = value;
            NotifyObservers();
        } 
    }

    public void OnGamePaused()
    {
        //Time.timeScale = 0;
        IsPaused = true;
    }

    public void OnGameResume()
    {
        IsPaused = false;
        //Time.timeScale = 1;
    }

    public void OnGameOver()
    {
        gameOverMenu.ShowMenu();
    }

    private void NotifyObservers()
    {
        foreach (var observer in observers)
        {
            observer.OnGamePaused(IsPaused);
        }
    }

    public void RegisterObserver(IGameObserver observer)
    {
        observers.Add(observer);
    }

    public void UnregisterObserver(IGameObserver observer)
    {
        observers.Remove(observer);
    }
}
