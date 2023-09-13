

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfigs", menuName = "Configs/Level")]
public class LevelConfigs : ScriptableObject
{
    public List<LevelConfig> _levelConfigs;
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
}

[System.Serializable]
public class LevelConfig
{
    public GameObject levelPrefab;
}
