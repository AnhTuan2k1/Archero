
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class AbilityManager: MonoBehaviour
{
    [SerializeField] AbilityMenu menu;

    private static AbilityManager _instance;
    public static AbilityManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<AbilityManager>();
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    public AbilityConfig getAbilityConfig(int index)
    {
        return AbilityConfigs.Instance.getAbilityConfig(index);
    }

    public AbilityConfig getAnRandomAbilityConfig()
    {
        Random random = new Random();
        int randomNumber = random.Next(0, AbilityConfigs.Instance._abilityConfigs.Length);
        return AbilityConfigs.Instance.getAbilityConfig(randomNumber);
    }

    public Sprite getSpriteAbilityConfig(AbilityType type)
    {
        return AbilityConfigs.Instance._abilityConfigs
            .First((a) => a.AbilityType == type).image;
    }

    public void ShowAbilityMenu() => menu.ShowMenu();
}
