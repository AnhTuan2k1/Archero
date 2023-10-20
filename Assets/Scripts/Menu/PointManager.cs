
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PointManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textplayerExpPoint;
    [SerializeField] protected Slider expBar;
    private float playerExpPoint;

    [SerializeField] private TextMeshProUGUI textPlayerLevel;
    private int playerLevel;
    public int PlayerLevel 
    {
        get => playerLevel; 
        private set
        {
            playerLevel = value;
            textPlayerLevel.text = "Level " + value;
        }
    }

    [SerializeField] private TextMeshProUGUI textpoint;
    private int point;
    public int Point
    {
        get => point;
        private set
        {
            point = value;
            textpoint.text = value.ToString();
        }
    }

    private void Awake()
    {
        PlayerLevel = 1;
        Point = 0;
    }


    private static PointManager _instance;
    public static PointManager Instance
    {
        get
        {
            if (_instance == null)
                return FindObjectOfType<PointManager>();
            return _instance;
        }
    }

    public void GetExpPoint(float pointExp)
    {
        playerExpPoint += pointExp;
        float expLevelUp = PlayerLevel * 5 + 50 * (1 + PlayerLevel / 10);

        if (playerExpPoint > expLevelUp)
        {
            playerExpPoint -= expLevelUp;

            PlayerLevel++;
            AbilityManager.Instance.ShowAbilityMenu();
        }

        expBar.value = playerExpPoint / expLevelUp;
    }

    public void AddPoint(int point)
    {
        Point += point;
    }
}
