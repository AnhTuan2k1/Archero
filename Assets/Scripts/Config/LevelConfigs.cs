

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfigs", menuName = "Configs/Level")]
public class LevelConfigs : ScriptableObject
{
    [SerializeField] private List<LevelConfig> _levelConfigs;
    private static LevelConfigs _instance;
    public static LevelConfigs Instance
    {
        get
        {
            if (_instance == null)
                _instance = Resources.Load<LevelConfigs>(path: "Config/LevelConfigs");
            return _instance;
        }
    }

    public LevelConfig getLevelConfig(int level)
    {
        return _levelConfigs[level];
    }

    public LevelConfig GetRandomLevelConfig()
    {
        return _levelConfigs[Random.Range(1, _levelConfigs.Count)];
    }

    public int GetLevelConfigCount()
    {
        return _levelConfigs.Count;
    }
}

[System.Serializable]
public class LevelConfig
{
    public int levelID;
    public GameObject levelPrefab;
}
