using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityMenu : MonoBehaviour
{
    [SerializeField] GameObject abilityTemplate;
    [SerializeField] Transform parentTransform;
    [SerializeField] bool reroll;

    private void Start()
    {
        gameObject.SetActive(false);

        Invoke(nameof(ShowMenu), 1);
    }

    public void ShowMenu()
    {
        reroll = true;
        gameObject.SetActive(true);

        GameManager.Instance.OnGamePaused();
        ReRollAbility();
        reroll = true;
    }

    public void HideMenu()
    {
        gameObject.SetActive(false);

        GameManager.Instance.OnGameResume();
        DestroyAbility();
    }

    private void DestroyAbility()
    {
        int childCount = parentTransform.childCount;

        for (int i = childCount - 1; i >= 0; i--)
        {
            Transform child = parentTransform.GetChild(i);
            Destroy(child.gameObject);
            //or DestroyImmediate(child.gameObject);
        }
    }

    public void SpawnAbility()
    {
        GameObject g;
        for (int i = 0; i < 3; i++)
        {
            g = Instantiate(abilityTemplate, parentTransform);
            AbilityConfig ability = AbilityManager.Instance.getAnRandomAbilityConfig();

            g.transform.GetChild(0).GetComponent<Image>().sprite = ability.image;
            g.transform.GetChild(1).GetComponent<TMP_Text>().text = ability.title;
            g.transform.GetChild(2).GetComponent<TMP_Text>().text = ability.description;
            g.GetComponent<Button>().AddEventListener(ability.AbilityType, AbilityClicked);
        }
    }

    public void ReRollAbility()
    {
        if (reroll)
        {
            DestroyAbility();
            SpawnAbility();
            reroll = false;
        }
    }

    private void AbilityClicked(AbilityType type)
    {
        HideMenu();

        Player.Instance.AddAbility(type);
    }
}

public static class ButtonExtention
{
    public static void AddEventListener<T> (this Button button, T param, Action<T> OnClick)
    {
        button.onClick.AddListener(delegate ()
        {
            OnClick(param);
        });
    }
}