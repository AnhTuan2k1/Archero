
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityConfigs", menuName = "Configs/Ability")]
public class AbilityConfigs : ScriptableObject
{
    public AbilityConfig[] _abilityConfigs;
    private static AbilityConfigs _instance;
    public static AbilityConfigs Instance
    {
        get
        {
            if (_instance == null)
                _instance = Resources.Load<AbilityConfigs>(path: "Config/AbilityConfigs");
            return _instance;
        }
    }

    public AbilityConfig getAbilityConfig(int index)
    {
        return _abilityConfigs[index];
    }

}

[System.Serializable]
public class AbilityConfig
{
    public AbilityType AbilityType;
    public Sprite image;
    public string title;
    public string description;
}
