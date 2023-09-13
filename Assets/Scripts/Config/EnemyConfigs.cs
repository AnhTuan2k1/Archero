


using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfigs", menuName = "Configs/Enemy")]
public class EnemyConfigs : ScriptableObject
{
    public List<EnemyConfig> _enemyConfigs;
    private static EnemyConfigs _instance;
    public static EnemyConfigs Instance
    {
        get
        {
            if (_instance == null)
                _instance = Resources.Load<EnemyConfigs>(path: "Config/EnemyConfigs");
            return _instance; 
        }
    }

    public EnemyConfig GetEnemyConfig(int id)
    {
        return _enemyConfigs.Find(e => e.Id == id);
    }
}

[System.Serializable]
public class EnemyConfig
{
    public int Id;
    //public float Hp;
    //public float Speed;
    //public float Damage;
    //public Enemy EnemyPrefab;
}