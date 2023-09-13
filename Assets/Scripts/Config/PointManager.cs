
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PointManager : MonoBehaviour
{
    public Player player;
    public int PlayerLevel { get; private set; }
    private float playerExpPoint;
    [SerializeField] protected Slider expBar;

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

        expBar.value = playerExpPoint/(PlayerLevel * 22 + 20);
    }

}
