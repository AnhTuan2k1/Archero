


using System;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;
    public bool IsReadyForNewLevel { get; private set; } = false;
    public static LevelManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<LevelManager>();
            return _instance;
        }
    }

    [SerializeField] private int currentLevel;
    private GameObject currentLevelObject;
    public int CurrentLevel { get => currentLevel;}

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        SpawnLevel(0);
    }

    private void SpawnLevel(int level)
    {
        if (currentLevelObject != null) Destroy(currentLevelObject);

        LevelConfig levelConfig = LevelConfigs.Instance.getLevelConfig(level);
        currentLevelObject = Instantiate(levelConfig.levelPrefab);

        currentLevel = level;
        if (level == 0) IsReadyForNewLevel = true;
        else IsReadyForNewLevel = false;
    }

    public void SpawnNextLevel()
    {
        if (LevelConfigs.Instance._levelConfigs.Count > currentLevel)
            SpawnLevel(currentLevel + 1);
    }

    public bool SetIsReadyForNewLevel(bool Iseady)
    {
        IsReadyForNewLevel = Iseady;
        return IsReadyForNewLevel;
    }
}
