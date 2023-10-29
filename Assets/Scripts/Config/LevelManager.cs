

using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

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

    private GameObject currentLevelObject;
    public List<LevelConfig> levels;
    [SerializeField] private GameObject maps;

    [SerializeField] private TextMeshProUGUI textcurrentMap;
    [SerializeField] private int currentLevel;
    public int CurrentLevel 
    { 
        get => currentLevel;
        private set
        {
            currentLevel = value;

            textcurrentMap.text = 
                (value/10).ToString() + " - " + value.ToString()[^1];
        }
    }

    private void Awake()
    {
        _instance = this;
        levels = new List<LevelConfig>();
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

        CurrentLevel = level;
        if (level == 0) IsReadyForNewLevel = true;
        else IsReadyForNewLevel = false;
    }

    private void SpawnNewLevel(int level)
    {
        currentLevelObject.SetActive(false);

        //get level map
        LevelConfig newLevelConfig = LevelConfigs.Instance.GetRandomLevelConfig();
        LevelConfig[] oldLevelConfig = 
            levels.Where(l => l.levelID == newLevelConfig.levelID).ToArray();
        if (oldLevelConfig.Length > 0)
        {
            currentLevelObject = oldLevelConfig[0].levelPrefab;
        }
        else
        {
            currentLevelObject = Instantiate(newLevelConfig.levelPrefab);
            currentLevelObject.transform.SetParent(maps.transform);
            LevelConfig l = new()
            {
                levelID = newLevelConfig.levelID,
                levelPrefab = currentLevelObject
            };
            levels.Add(l);
        }

        currentLevelObject.SetActive(true);


        // instantiate enemy.dependent note: enemy gameObject
        Transform enemyPosition = currentLevelObject.transform.GetChild(0);
        foreach (Transform enemyPos in enemyPosition)
        {
            Enemy.Instantiate(enemyPos.position,
                EnemyManager.Instance.RandomAnEnemyType(level));
        }

        CurrentLevel = level;
        if (level == 0) IsReadyForNewLevel = true;
        else IsReadyForNewLevel = false;
    }

    private void SpawnBossLevel(int level)
    {
        currentLevelObject.SetActive(false);

        //get level map
        LevelConfig newLevelConfig = LevelConfigs.Instance.GetBossLevelConfig();
        LevelConfig[] oldLevelConfig =
            levels.Where(l => l.levelID == newLevelConfig.levelID).ToArray();
        if (oldLevelConfig.Length > 0)
        {
            currentLevelObject = oldLevelConfig[0].levelPrefab;
        }
        else
        {
            currentLevelObject = Instantiate(newLevelConfig.levelPrefab);
            currentLevelObject.transform.SetParent(maps.transform);
            LevelConfig l = new()
            {
                levelID = newLevelConfig.levelID,
                levelPrefab = currentLevelObject
            };
            levels.Add(l);
        }

        currentLevelObject.SetActive(true);
        EnemyManager.Instance.InstantiateBoss(level);

        CurrentLevel = level;
        IsReadyForNewLevel = false;
    }

    public void SpawnNextLevel()
    {
        if (LevelConfigs.Instance.GetLevelConfigCount() > CurrentLevel)
            SpawnLevel(CurrentLevel + 1);
    }

    public void SpawnEndlessLevel()
    {
        if (CurrentLevel % 10 == 9) SpawnBossLevel(CurrentLevel + 1);
        else SpawnNewLevel(CurrentLevel + 1);
    }

    public bool SetIsReadyForNewLevel(bool Iseady)
    {
        IsReadyForNewLevel = Iseady;
        return IsReadyForNewLevel;
    }
}
