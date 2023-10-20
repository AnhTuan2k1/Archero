
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
        //enemies.RemoveAll(e => !e.gameObject.activeInHierarchy);

        if (enemies.Count == 0)
            LevelManager.Instance.SetIsReadyForNewLevel(true);
    }

    public void Clean()
    {
        enemies.Clear();
        enemies = new List<Enemy>();
        LevelManager.Instance.SetIsReadyForNewLevel(true);
    }

    public void AddEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
    }

    public ObjectPoolingType RandomAnEnemyType()
    {
        switch (Random.Range(1, 3))
        {
            case 1:
                return ObjectPoolingType.Enemy2001;
            case 2:
                return ObjectPoolingType.Bat;
            default:
                return ObjectPoolingType.Enemy2001;
        }
    }
}
