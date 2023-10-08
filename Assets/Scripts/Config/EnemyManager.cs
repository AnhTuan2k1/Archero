
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private static EnemyManager _instance;
    public static EnemyManager Instance
    {
        get 
        { 
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<EnemyManager>();
            return _instance; 
        }
    }

    public List<Enemy> Enemies { get => enemies;}
    [SerializeField] private List<Enemy> enemies;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        enemies ??= new List<Enemy>();

    }

    public void RemoveEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);
        GameManager.Instance.UnregisterObserver(enemy);

        //if (enemies.Count == 0) 
        //    LevelManager.Instance.SpawnNextLevel();
        if (enemies.Count == 0)
            LevelManager.Instance.SetIsReadyForNewLevel(true);
    }

    public void AddEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
        GameManager.Instance.RegisterObserver(enemy);
    }
}
