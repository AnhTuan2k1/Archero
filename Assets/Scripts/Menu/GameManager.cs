using UnityEngine;
using System;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public List<IGameObserver> observers = new List<IGameObserver>();
    [SerializeField] public bool IsPaused { get; private set; } = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
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

    public void OnGamePaused()
    {
        Time.timeScale = 0;
    }

    public void OnGameResume()
    {
        Time.timeScale = 1;
    }

    //public void TogglePause()
    //{
    //    IsPaused = !IsPaused;
    //    NotifyObservers();
    //}

    private void NotifyObservers()
    {
        foreach (var observer in observers)
        {
            observer.OnGamePaused(IsPaused);
        }
    }
}
