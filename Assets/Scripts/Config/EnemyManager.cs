
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
    public List<BossEnemy> BossPrefab; 
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

    public Vector3 Clean()
    {
        if(enemies.Count != 0)
        {
            foreach (Enemy enemy in enemies)
            {
                if (enemy.isActiveAndEnabled)
                {
                    return enemy.transform.position;
                }
            }
        }

        enemies.Clear();
        enemies = new List<Enemy>();
        LevelManager.Instance.SetIsReadyForNewLevel(true);
        return Vector3.zero;
    }

    public void AddEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
    }

    public ObjectPoolingType RandomAnEnemyType(int level)
    {
        int random = Random.Range(1, 3 + level/10);

        if (random == 0) return RandomAnEnemy(4);
        else if (random == 1)
            return ObjectPoolingType.Enemy2001;
        else if (random == 2 || random == 5 || random == 8)
            return ObjectPoolingType.Bat;
        else if (random == 3 || random == 6 || random == 9)
            return ObjectPoolingType.GrimReaper;
        else if (random == 4 || random == 7 || random == 10)
            return ObjectPoolingType.SuperBat;

        else return ObjectPoolingType.SuperBat;
    }

    public ObjectPoolingType RandomAnEnemy(int enemyNum)
    {
        int random = Random.Range(1, 100);
        switch(random % enemyNum + 1)
        {
            case 1:
                return ObjectPoolingType.Enemy2001;
            case 2:
                return ObjectPoolingType.Bat;
            case 3:
                return ObjectPoolingType.GrimReaper;
            case 4:
                return ObjectPoolingType.SuperBat;
            default:
                return ObjectPoolingType.SuperBat;
        }
    }

    public void InstantiateBoss(int level)
    {
        if (level == 10) Instantiate(BossPrefab[0]);
        else if(level == 20) Instantiate(BossPrefab[1]);
        else Instantiate(BossPrefab[Random.Range(0, BossPrefab.Count)]);

    }
}
