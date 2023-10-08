
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PointManager : MonoBehaviour
{
    private int playerLevel;
    public int PlayerLevel { get => playerLevel; private set => playerLevel = value; }
    private float playerExpPoint;
    [SerializeField] protected Slider expBar;
    [SerializeField] private TextMeshProUGUI textPlayerLevel;

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
        if (playerExpPoint > PlayerLevel * 22 + 20)
        {
            playerExpPoint -= PlayerLevel * 22 + 20;

            PlayerLevel++;
            AbilityManager.Instance.ShowAbilityMenu();
        }

        expBar.value = playerExpPoint / (PlayerLevel * 22 + 20);
    }

    public void UpDatePoint()
    {
        
    }
}
